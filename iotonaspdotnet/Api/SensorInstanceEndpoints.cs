using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class SensorInstanceEndpoints
{
    public static IEndpointRouteBuilder MapSensorInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/sensorInstance").WithTags("SensorInstances");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);

    group.MapDelete("/", addToTelemetryStreams);
    group.MapDelete("/", removeFromTelemetryStreams);


        return app;
    }

    private static async Task<IResult> Create(
        SensorInstanceRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        SensorInstanceRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            var updated = await service.UpdateAsync(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetAll(
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( SensorInstanceResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var sensorInstance = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return sensorInstance is null ? Results.NotFound() : Results.Ok( sensorInstance );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    ISensorInstanceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignTelemetryStreams(
        AssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToTelemetryStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTelemetryStreams(
        AssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromTelemetryStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private SensorInstance mapRequestToSensorInstance( SensorInstanceRequest request ) {
        var model = new SensorInstance
        {
            Id = request.id,
            Name = request.Name,
            Unit = request.Unit,
            SamplingIntervalMs = request.SamplingIntervalMs,
            Device = request.Device,
            TelemetryStreams = request.TelemetryStreams,
            SensorType = request.SensorType,
        }
        return model;
    }
}
