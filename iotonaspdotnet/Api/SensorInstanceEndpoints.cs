using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);

    group.MapPut("/", addToTelemetryStreams);
    group.MapPut("/", removeFromTelemetryStreams);


        return app;
    }

    private static async Task<IResult> Create(
        SensorInstanceRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(model, cancellationToken);
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
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var sensorInstance = await service.Get(identifier, cancellationToken);
        return sensorInstance is null ? Results.NotFound() : Results.Ok( sensorInstance );
    }


    private static async Task<IResult> GetAll(
        ISensorInstanceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SensorInstanceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    ISensorInstanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTelemetryStreams(
        MultipleAssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTelemetryStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTelemetryStreams(
        MultipleAssociationRequest request,
        ISensorInstanceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTelemetryStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SensorInstance mapRequestToSensorInstance( SensorInstanceRequest request ) {
        var model = new SensorInstance
        {
            Id = request.Id,
            Name = request.Name,
            Unit = request.Unit,
            SamplingIntervalMs = request.SamplingIntervalMs,
            SensorType = request.SensorType,
        };
        return model;
    }

}
