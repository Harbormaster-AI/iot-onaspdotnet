using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class DeviceVendorEndpoints
{
    public static IEndpointRouteBuilder MapDeviceVendorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceVendor").WithTags("DeviceVendors");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IDeviceVendorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IDeviceVendorService service,
        CancellationToken cancellationToken)
    {
        var deviceVendor = await service.GetByIdAsync(id, cancellationToken);
        return deviceVendor is null ? Results.NotFound() : Results.Ok(ToResponse( deviceVendor ));
    }

    private static async Task<IResult> Create(
        CreateDeviceVendorRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken)
    {
        var deviceVendor = new DeviceVendor
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                LegalName = request.LegalName,
                HeadquartersCountry = request.HeadquartersCountry,
                Website = request.Website,


        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/deviceVendors/deviceVendor.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateDeviceVendorRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DeviceVendor
        {
            Id = id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,

        };

        try
        {
            var updated = await service.UpdateAsync(lowercaseClassName, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> Delete(
        Guid id,
        IDeviceVendorService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static DeviceVendorResponse ToResponse(DeviceVendor lowercaseClassName)
        => new( deviceVendor.Id,
                , String, String, String, String
                 );

}
