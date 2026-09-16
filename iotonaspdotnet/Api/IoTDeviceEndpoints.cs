using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class IoTDeviceEndpoints
{
    public static IEndpointRouteBuilder MapIoTDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/ioTDevice").WithTags("IoTDevices");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IIoTDeviceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IIoTDeviceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateIoTDeviceRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new IoTDevice
        {
            Id = Guid.NewGuid(),

                DeviceId = request.DeviceId,
                SerialNumber = request.SerialNumber,
                LastSeen = request.LastSeen,
                FirmwareVersion = request.FirmwareVersion,
                Status = request.Status,
                PowerSource = request.PowerSource,

                DeviceModelId = request.DeviceModelId,
                TenantId = request.TenantId,
                SiteId = request.SiteId,
                RoomId = request.RoomId,
                GatewayId = request.GatewayId,
                DigitalTwinId = request.DigitalTwinId,
                ProvisioningRecordId = request.ProvisioningRecordId,

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
        UpdateIoTDeviceRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new IoTDevice
        {
            Id = id,
            IoTDeviceNumber = request.IoTDeviceNumber,
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
        IIoTDeviceService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static IoTDeviceResponse ToResponse(IoTDevice lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.IoTDeviceNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
