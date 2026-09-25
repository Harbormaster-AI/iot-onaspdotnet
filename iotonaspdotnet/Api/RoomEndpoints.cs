
using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class RoomEndpoints
{
    public static IEndpointRouteBuilder MapRoomEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/room").WithTags("Rooms");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignFloor", AssignFloor);
        group.MapPut("/unassignFloor", UnassignFloor);

    group.MapPut("/addToDevices", AddToDevices);
    group.MapPut("/removeFromDevices", RemoveFromDevices);

    group.MapPut("/addToGateways", AddToGateways);
    group.MapPut("/removeFromGateways", RemoveFromGateways);


        return app;
    }

    private static async Task<IResult> Create(
        RoomRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRoom( request );

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
        RoomRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRoom( request );

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
        IRoomService service,
        CancellationToken cancellationToken) {

        var room = await service.Get(identifier, cancellationToken);
        return room is null ? Results.NotFound() : Results.Ok( room );
    }


    private static async Task<IResult> GetAll(
        IRoomService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RoomResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRoomService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFloor(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFloor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFloor(
    AssociationRequest request,
    IRoomService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFloor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGateways(
        MultipleAssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGateways(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGateways(
        MultipleAssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGateways(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Room mapRequestToRoom( RoomRequest request ) {
        var model = new Room
        {
            Id = request.Id,
            Name = request.Name,
        };
        return model;
    }

}
