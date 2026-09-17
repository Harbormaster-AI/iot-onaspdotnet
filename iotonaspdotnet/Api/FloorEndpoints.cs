using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignBuilding);
        group.MapDelete("/", unassignBuilding);

    group.MapDelete("/", addToRooms);
    group.MapDelete("/", removeFromRooms);


        return app;
    }

    private static async Task<IResult> Create(
        FloorRequest request,
        IFloorService service,
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
        FloorRequest request,
        IFloorService service,
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
        IFloorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( FloorResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IFloorService service,
        CancellationToken cancellationToken) {

        var floor = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return floor is null ? Results.NotFound() : Results.Ok( floor );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFloorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBuilding(
        AssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignBuildingAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBuilding(
    AssociationRequest request,
    IFloorService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignBuildingAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignRooms(
        AssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToRoomsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRooms(
        AssociationRequest request,
        IFloorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromRoomsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Floor mapRequestToFloor( FloorRequest request ) {
        var model = new Floor
        {
            Id = request.id,
            Name = request.Name;
            Level = request.Level;
            Building = request.Building;
            Rooms = request.Rooms;
        }
        return model;
    }
}
