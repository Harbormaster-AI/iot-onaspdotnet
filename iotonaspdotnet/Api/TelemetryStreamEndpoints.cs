using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TelemetryStreamEndpoints
{
    public static IEndpointRouteBuilder MapTelemetryStreamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetryStream").WithTags("TelemetryStreams");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/", AssignDevice);
        group.MapPut("/", UnassignDevice);
        group.MapPut("/", AssignSensor);
        group.MapPut("/", UnassignSensor);
        group.MapPut("/", AssignSchema);
        group.MapPut("/", UnassignSchema);
        group.MapPut("/", AssignMessagingEndpoint);
        group.MapPut("/", UnassignMessagingEndpoint);
        group.MapPut("/", AssignRetentionPolicy);
        group.MapPut("/", UnassignRetentionPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTelemetryStream( request );

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
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTelemetryStream( request );

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


    private static TelemetryStream mapRequestToTelemetryStream( TelemetryStreamRequest request ) {
        var model = new TelemetryStream
        {
            Id = request.Id,
            StreamName = request.StreamName,
            RetentionDays = request.RetentionDays,
            Qos = request.Qos,
        };
        return model;
    }

}
