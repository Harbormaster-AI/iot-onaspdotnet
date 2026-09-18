using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignGateway);
        group.MapPut("/", unassignGateway);


        return app;
    }

    private static async Task<IResult> Create(
        EdgeApplicationRequest request,
        IEdgeApplicationService service,
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
        EdgeApplicationRequest request,
        IEdgeApplicationService service,
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
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {

        var edgeApplication = await service.Get(identifier, cancellationToken);
        return edgeApplication is null ? Results.NotFound() : Results.Ok( edgeApplication );
    }


    private static async Task<IResult> GetAll(
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EdgeApplicationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IEdgeApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGateway(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGateway(
    AssociationRequest request,
    IEdgeApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGateway(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static EdgeApplication mapRequestToEdgeApplication( EdgeApplicationRequest request ) {
        var model = new EdgeApplication
        {
            Id = request.id,
            Name = request.Name,
            Version = request.Version,
            Image = request.Image,
            Gateway = request.Gateway,
            Status = request.Status,
        };
        return model;
    }

}
