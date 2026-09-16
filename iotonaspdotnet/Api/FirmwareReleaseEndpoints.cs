using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class FirmwareReleaseEndpoints
{
    public static IEndpointRouteBuilder MapFirmwareReleaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/firmwareRelease").WithTags("FirmwareReleases");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IFirmwareReleaseService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateFirmwareReleaseRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new FirmwareRelease
        {
            Id = Guid.NewGuid(),

                Version = request.Version,
                ReleaseDate = request.ReleaseDate,
                ReleaseNotes = request.ReleaseNotes,
                Checksum = request.Checksum,

                DeviceModelId = request.DeviceModelId

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/lowercaseClassNames/{lowercaseClassName.Id}", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateFirmwareReleaseRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new FirmwareRelease
        {
            Id = id,
            FirmwareReleaseNumber = request.FirmwareReleaseNumber,
            Balance = request.Balance,
            CustomerId = request.CustomerId
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
        IFirmwareReleaseService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static FirmwareReleaseResponse ToResponse(FirmwareRelease lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.FirmwareReleaseNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
