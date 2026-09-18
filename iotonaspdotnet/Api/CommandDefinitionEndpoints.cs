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

        group.MapPut("/", assignDeviceModel);
        group.MapPut("/", unassignDeviceModel);

    group.MapPut("/", addToActuators);
    group.MapPut("/", removeFromActuators);

    group.MapPut("/", addToCommandInvocations);
    group.MapPut("/", removeFromCommandInvocations);


        return app;
    }

    private static async Task<IResult> Create(
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
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
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
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
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var commandDefinition = await service.Get(identifier, cancellationToken);
        return commandDefinition is null ? Results.NotFound() : Results.Ok( commandDefinition );
    }


    private static async Task<IResult> GetAll(
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CommandDefinitionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
        AssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeviceModel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeviceModel(
    AssociationRequest request,
    ICommandDefinitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeviceModel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToActuators(
        MultipleAssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToActuators(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromActuators(
        MultipleAssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromActuators(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCommandInvocations(
        MultipleAssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCommandInvocations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCommandInvocations(
        MultipleAssociationRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCommandInvocations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private CommandDefinition mapRequestToCommandDefinition( CommandDefinitionRequest request ) {
        var model = new CommandDefinition
        {
            Id = request.id,
            Name = request.Name,
            RequestSchemaUri = request.RequestSchemaUri,
            ResponseSchemaUri = request.ResponseSchemaUri,
            TimeoutSeconds = request.TimeoutSeconds,
            DeviceModel = request.DeviceModel,
            Actuators = request.Actuators,
            CommandInvocations = request.CommandInvocations,
        }
        return model;
    }

}
