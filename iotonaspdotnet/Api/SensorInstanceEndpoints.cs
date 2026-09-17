using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class SensorInstanceEndpoints
{
    public static IEndpointRouteBuilder MapSensorInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sensorInstance").WithTags("SensorInstances");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ISensorInstanceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISensorInstanceService service,
        CancellationToken cancellationToken)
    {
        var sensorInstance = await service.GetByIdAsync(id, cancellationToken);
        return sensorInstance is null ? Results.NotFound() : Results.Ok(ToResponse( sensorInstance ));
    }

    private static async Task<IResult> Create(
        CreateSensorInstanceRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken)
    {
        var sensorInstance = new SensorInstance
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                Unit = request.Unit,
                SamplingIntervalMs = request.SamplingIntervalMs,
                SensorType = request.SensorType,

                IoTDeviceId = request.IoTDeviceId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/sensorInstances/sensorInstance.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateSensorInstanceRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SensorInstance
        {
            Id = id,
            Name = request.Name,
            Unit = request.Unit,
            SamplingIntervalMs = request.SamplingIntervalMs,
            SensorType = request.SensorType,

            IoTDeviceId = request.IoTDeviceId,
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
        ISensorInstanceService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static SensorInstanceResponse ToResponse(SensorInstance lowercaseClassName)
        => new( sensorInstance.Id,
                , String, String, Integer, SensorType
                , IoTDeviceId );

}
