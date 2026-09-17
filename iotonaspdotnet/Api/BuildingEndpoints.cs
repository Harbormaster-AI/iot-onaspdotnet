using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class BuildingEndpoints
{
    public static IEndpointRouteBuilder MapBuildingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/building").WithTags("Buildings");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignSite);
        group.MapDelete("/", unassignSite);

    group.MapDelete("/", addToFloors);
    group.MapDelete("/", removeFromFloors);


        return app;
    }

    private static async Task<IResult> Create(
        BuildingRequest request,
        IBuildingService service,
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
        BuildingRequest request,
        IBuildingService service,
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
        IBuildingService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( BuildingResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IBuildingService service,
        CancellationToken cancellationToken) {

        var building = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return building is null ? Results.NotFound() : Results.Ok( building );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSiteAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
    AssociationRequest request,
    IBuildingService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSiteAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignFloors(
        AssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToFloorsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFloors(
        AssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromFloorsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@6a1e8848 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@6a1e8848( com.harbormaster.codetemplate.model.classes.ClassObject@6a1e8848Request request ) {
        var model = new Building
        {
            Id = request.id,
        Name = request.Name
        Site = request.Site
        Floors = request.Floors
        }
        return model;
    }
}
