using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class EdgeApplicationEndpoints
{
    public static IEndpointRouteBuilder MapEdgeApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/edgeApplication").WithTags("EdgeApplications");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignGateway);
        group.MapDelete("/", unassignGateway);


        return app;
    }

    private static async Task<IResult> Create(
        EdgeApplicationRequest request,
        IEdgeApplicationService service,
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
        EdgeApplicationRequest request,
        IEdgeApplicationService service,
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
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( EdgeApplicationResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {

        var edgeApplication = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return edgeApplication is null ? Results.NotFound() : Results.Ok( edgeApplication );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
    AssociationRequest request,
    IEdgeApplicationService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
