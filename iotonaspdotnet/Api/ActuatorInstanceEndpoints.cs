using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class ActuatorInstanceEndpoints
{
    public static IEndpointRouteBuilder MapActuatorInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/actuatorInstance").WithTags("ActuatorInstances");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);

    group.MapPut("/", addToSupportedCommands);
    group.MapPut("/", removeFromSupportedCommands);


        return app;
    }

    private static async Task<IResult> Create(
        ActuatorInstanceRequest request,
        IActuatorInstanceService service,
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
        ActuatorInstanceRequest request,
        IActuatorInstanceService service,
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
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {

        var actuatorInstance = await service.Get(identifier, cancellationToken);
        return actuatorInstance is null ? Results.NotFound() : Results.Ok( actuatorInstance );
    }


    private static async Task<IResult> GetAll(
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ActuatorInstanceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IActuatorInstanceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSupportedCommands(
        MultipleAssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSupportedCommands(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSupportedCommands(
        MultipleAssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSupportedCommands(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private ActuatorInstance mapRequestToActuatorInstance( ActuatorInstanceRequest request ) {
        var model = new ActuatorInstance
        {
            Id = request.id,
            Name = request.Name,
            CommandTopic = request.CommandTopic,
            Device = request.Device,
            SupportedCommands = request.SupportedCommands,
            ActuatorType = request.ActuatorType,
        };
        return model;
    }

}
