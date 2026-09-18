using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TelemetryStreamEndpoints
{
    public static IEndpointRouteBuilder MapTelemetryStreamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetryStream").WithTags("TelemetryStreams");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignSensor);
        group.MapPut("/", unassignSensor);
        group.MapPut("/", assignSchema);
        group.MapPut("/", unassignSchema);
        group.MapPut("/", assignMessagingEndpoint);
        group.MapPut("/", unassignMessagingEndpoint);
        group.MapPut("/", assignRetentionPolicy);
        group.MapPut("/", unassignRetentionPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
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
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var telemetryStream = await service.Get(identifier, cancellationToken);
        return telemetryStream is null ? Results.NotFound() : Results.Ok( telemetryStream );
    }


    private static async Task<IResult> GetAll(
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TelemetryStreamResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSensor(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSensor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSensor(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSensor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSchema(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSchema(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSchema(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSchema(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMessagingEndpoint(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMessagingEndpoint(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMessagingEndpoint(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMessagingEndpoint(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionPolicy(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRetentionPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRetentionPolicy(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRetentionPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private TelemetryStream mapRequestToTelemetryStream( TelemetryStreamRequest request ) {
        var model = new TelemetryStream
        {
            Id = request.id,
            StreamName = request.StreamName,
            RetentionDays = request.RetentionDays,
            Device = request.Device,
            Sensor = request.Sensor,
            Schema = request.Schema,
            MessagingEndpoint = request.MessagingEndpoint,
            RetentionPolicy = request.RetentionPolicy,
            Qos = request.Qos,
        };
        return model;
    }

}
