using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class CommandDefinitionEndpoints
{
    public static IEndpointRouteBuilder MapCommandDefinitionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandDefinition").WithTags("CommandDefinitions");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignDeviceModel);
        group.MapDelete("/", unassignDeviceModel);

    group.MapDelete("/", addToActuators);
    group.MapDelete("/", removeFromActuators);

    group.MapDelete("/", addToCommandInvocations);
    group.MapDelete("/", removeFromCommandInvocations);


        return app;
    }

    private static async Task<IResult> Create(
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
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
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
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
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( CommandDefinitionResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var commandDefinition = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return commandDefinition is null ? Results.NotFound() : Results.Ok( commandDefinition );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceModelAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
    AssociationRequest request,
    ICommandDefinitionService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceModelAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignActuators(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToActuatorsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignActuators(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromActuatorsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@33a96701 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@33a96701( com.harbormaster.codetemplate.model.classes.ClassObject@33a96701Request request ) {
        var model = new CommandDefinition
        {
            Id = request.id,
        Name = request.Name
        RequestSchemaUri = request.RequestSchemaUri
        ResponseSchemaUri = request.ResponseSchemaUri
        TimeoutSeconds = request.TimeoutSeconds
        DeviceModel = request.DeviceModel
        Actuators = request.Actuators
        CommandInvocations = request.CommandInvocations
        }
        return model;
    }
    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCommandInvocationsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCommandInvocationsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@33a96701 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@33a96701( com.harbormaster.codetemplate.model.classes.ClassObject@33a96701Request request ) {
        var model = new CommandDefinition
        {
            Id = request.id,
        Name = request.Name
        RequestSchemaUri = request.RequestSchemaUri
        ResponseSchemaUri = request.ResponseSchemaUri
        TimeoutSeconds = request.TimeoutSeconds
        DeviceModel = request.DeviceModel
        Actuators = request.Actuators
        CommandInvocations = request.CommandInvocations
        }
        return model;
    }
}
