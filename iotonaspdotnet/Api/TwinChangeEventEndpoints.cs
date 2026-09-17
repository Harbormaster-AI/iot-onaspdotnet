using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignTwin);
        group.MapDelete("/", unassignTwin);


        return app;
    }

    private static async Task<IResult> Create(
        TwinChangeEventRequest request,
        ITwinChangeEventService service,
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
        TwinChangeEventRequest request,
        ITwinChangeEventService service,
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
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TwinChangeEventResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {

        var twinChangeEvent = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return twinChangeEvent is null ? Results.NotFound() : Results.Ok( twinChangeEvent );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwin(
        AssociationRequest request,
        ITwinChangeEventService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTwinAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwin(
    AssociationRequest request,
    ITwinChangeEventService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTwinAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
