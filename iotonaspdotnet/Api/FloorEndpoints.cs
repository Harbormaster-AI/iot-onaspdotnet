using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class FloorEndpoints
{
    public static IEndpointRouteBuilder MapFloorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/floor").WithTags("Floors");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignBuilding);
        group.MapPut("/", unassignBuilding);

    group.MapPut("/", addToRooms);
    group.MapPut("/", removeFromRooms);


        return app;
    }

    private static async Task<IResult> Create(
        FloorRequest request,
        IFloorService service,
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
        FloorRequest request,
        IFloorService service,
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
        IFloorService service,
        CancellationToken cancellationToken) {

        var floor = await service.Get(identifier, cancellationToken);
        return floor is null ? Results.NotFound() : Results.Ok( floor );
    }


    private static async Task<IResult> GetAll(
        IFloorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FloorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFloorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBuilding(
        AssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBuilding(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBuilding(
    AssociationRequest request,
    IFloorService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBuilding(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRooms(
        MultipleAssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRooms(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRooms(
        MultipleAssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRooms(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Floor mapRequestToFloor( FloorRequest request ) {
        var model = new Floor
        {
            Id = request.id,
            Name = request.Name,
            Level = request.Level,
        };
        return model;
    }

}
