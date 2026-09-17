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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);

    group.MapDelete("/", addToSupportedCommands);
    group.MapDelete("/", removeFromSupportedCommands);


        return app;
    }

    private static async Task<IResult> Create(
        ActuatorInstanceRequest request,
        IActuatorInstanceService service,
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
        ActuatorInstanceRequest request,
        IActuatorInstanceService service,
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
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( ActuatorInstanceResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {

        var actuatorInstance = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return actuatorInstance is null ? Results.NotFound() : Results.Ok( actuatorInstance );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    IActuatorInstanceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignSupportedCommands(
        AssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToSupportedCommandsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupportedCommands(
        AssociationRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromSupportedCommandsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@46fc5c8f mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@46fc5c8f( com.harbormaster.codetemplate.model.classes.ClassObject@46fc5c8fRequest request ) {
        var model = new ActuatorInstance
        {
            Id = request.id,
        Name = request.Name
        CommandTopic = request.CommandTopic
        Device = request.Device
        SupportedCommands = request.SupportedCommands
        ActuatorType = request.ActuatorType
        }
        return model;
    }
}
