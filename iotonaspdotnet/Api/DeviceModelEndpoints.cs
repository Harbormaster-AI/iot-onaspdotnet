using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class DeviceModelEndpoints
{
    public static IEndpointRouteBuilder MapDeviceModelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceModel").WithTags("DeviceModels");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IDeviceModelService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IDeviceModelService service,
        CancellationToken cancellationToken)
    {
        var deviceModel = await service.GetByIdAsync(id, cancellationToken);
        return deviceModel is null ? Results.NotFound() : Results.Ok(ToResponse( deviceModel ));
    }

    private static async Task<IResult> Create(
        CreateDeviceModelRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken)
    {
        var deviceModel = new DeviceModel
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                ModelNumber = request.ModelNumber,
                HardwareRevision = request.HardwareRevision,
                SupportedConnectivity = request.SupportedConnectivity,
                DefaultTelemetryEncoding = request.DefaultTelemetryEncoding,

                DeviceVendorId = request.DeviceVendorId,
                TwinTemplateId = request.TwinTemplateId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/deviceModels/deviceModel.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateDeviceModelRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DeviceModel
        {
            Id = id,
            Name = request.Name,
            ModelNumber = request.ModelNumber,
            HardwareRevision = request.HardwareRevision,
            SupportedConnectivity = request.SupportedConnectivity,
            DefaultTelemetryEncoding = request.DefaultTelemetryEncoding,

            DeviceVendorId = request.DeviceVendorId,
            TwinTemplateId = request.TwinTemplateId,
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
        IDeviceModelService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static DeviceModelResponse ToResponse(DeviceModel lowercaseClassName)
        => new( deviceModel.Id,
                , String, String, String, ConnectivityType, TelemetryEncoding
                , DeviceVendorId, TwinTemplateId );

}
