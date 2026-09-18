using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TwinChangeEventEndpoints
{
    public static IEndpointRouteBuilder MapTwinChangeEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/twinChangeEvent").WithTags("TwinChangeEvents");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignTwin);
        group.MapPut("/", unassignTwin);


        return app;
    }

    private static async Task<IResult> Create(
        TwinChangeEventRequest request,
        ITwinChangeEventService service,
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
        TwinChangeEventRequest request,
        ITwinChangeEventService service,
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
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {

        var twinChangeEvent = await service.Get(identifier, cancellationToken);
        return twinChangeEvent is null ? Results.NotFound() : Results.Ok( twinChangeEvent );
    }


    private static async Task<IResult> GetAll(
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TwinChangeEventResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwin(
        AssociationRequest request,
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTwin(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTwin(
    AssociationRequest request,
    ITwinChangeEventService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTwin(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static TwinChangeEvent mapRequestToTwinChangeEvent( TwinChangeEventRequest request ) {
        var model = new TwinChangeEvent
        {
            Id = request.Id,
            EventId = request.EventId,
            OccurredAt = request.OccurredAt,
            ChangeType = request.ChangeType,
        };
        return model;
    }

}
