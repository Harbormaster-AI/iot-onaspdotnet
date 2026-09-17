using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class RoomEndpoints
{
    public static IEndpointRouteBuilder MapRoomEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/room").WithTags("Rooms");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignFloor);
        group.MapDelete("/", unassignFloor);

    group.MapDelete("/", addToDevices);
    group.MapDelete("/", removeFromDevices);

    group.MapDelete("/", addToGateways);
    group.MapDelete("/", removeFromGateways);


        return app;
    }

    private static async Task<IResult> Create(
        RoomRequest request,
        IRoomService service,
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
        RoomRequest request,
        IRoomService service,
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
        IRoomService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( RoomResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IRoomService service,
        CancellationToken cancellationToken) {

        var room = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return room is null ? Results.NotFound() : Results.Ok( room );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRoomService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFloor(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignFloorAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFloor(
    AssociationRequest request,
    IRoomService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignFloorAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDevicesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDevicesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@127c461d mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@127c461d( com.harbormaster.codetemplate.model.classes.ClassObject@127c461dRequest request ) {
        var model = new Room
        {
            Id = request.id,
        Name = request.Name
        Floor = request.Floor
        Devices = request.Devices
        Gateways = request.Gateways
        }
        return model;
    }
    private static async Task<IResult> AssignGateways(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToGatewaysAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateways(
        AssociationRequest request,
        IRoomService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromGatewaysAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@127c461d mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@127c461d( com.harbormaster.codetemplate.model.classes.ClassObject@127c461dRequest request ) {
        var model = new Room
        {
            Id = request.id,
        Name = request.Name
        Floor = request.Floor
        Devices = request.Devices
        Gateways = request.Gateways
        }
        return model;
    }
}
