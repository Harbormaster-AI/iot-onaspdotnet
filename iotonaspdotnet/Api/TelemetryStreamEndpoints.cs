using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignSensor);
        group.MapDelete("/", unassignSensor);
        group.MapDelete("/", assignSchema);
        group.MapDelete("/", unassignSchema);
        group.MapDelete("/", assignMessagingEndpoint);
        group.MapDelete("/", unassignMessagingEndpoint);
        group.MapDelete("/", assignRetentionPolicy);
        group.MapDelete("/", unassignRetentionPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
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
        TelemetryStreamRequest request,
        ITelemetryStreamService service,
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
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TelemetryStreamResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {

        var telemetryStream = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return telemetryStream is null ? Results.NotFound() : Results.Ok( telemetryStream );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSensor(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSensorAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSensor(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSensorAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSchema(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSchemaAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSchema(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSchemaAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMessagingEndpoint(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignMessagingEndpointAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMessagingEndpoint(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignMessagingEndpointAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionPolicy(
        AssociationRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignRetentionPolicyAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRetentionPolicy(
    AssociationRequest request,
    ITelemetryStreamService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignRetentionPolicyAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
