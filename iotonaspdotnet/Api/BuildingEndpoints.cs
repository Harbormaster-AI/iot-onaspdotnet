using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignSite);
        group.MapPut("/", unassignSite);

    group.MapPut("/", addToFloors);
    group.MapPut("/", removeFromFloors);


        return app;
    }

    private static async Task<IResult> Create(
        BuildingRequest request,
        IBuildingService service,
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
        BuildingRequest request,
        IBuildingService service,
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
        IBuildingService service,
        CancellationToken cancellationToken) {

        var building = await service.Get(identifier, cancellationToken);
        return building is null ? Results.NotFound() : Results.Ok( building );
    }


    private static async Task<IResult> GetAll(
        IBuildingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BuildingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSite(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSite(
    AssociationRequest request,
    IBuildingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSite(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFloors(
        MultipleAssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFloors(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFloors(
        MultipleAssociationRequest request,
        IBuildingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFloors(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Building mapRequestToBuilding( BuildingRequest request ) {
        var model = new Building
        {
            Id = request.id,
            Name = request.Name,
            Site = request.Site,
            Floors = request.Floors,
        };
        return model;
    }

}
