using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class CommandDefinitionEndpoints
{
    public static IEndpointRouteBuilder MapCommandDefinitionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandDefinition").WithTags("CommandDefinitions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDeviceModel", AssignDeviceModel);
        group.MapPut("/unassignDeviceModel", UnassignDeviceModel);

    group.MapPut("/addToActuators", AddToActuators);
    group.MapPut("/removeFromActuators", RemoveFromActuators);

    group.MapPut("/addToCommandInvocations", AddToCommandInvocations);
    group.MapPut("/removeFromCommandInvocations", RemoveFromCommandInvocations);


        return app;
    }

    private static async Task<IResult> Create(
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCommandDefinition( request );

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
        CommandDefinitionRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCommandDefinition( request );

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
    private static CommandDefinition mapRequestToCommandDefinition( CommandDefinitionRequest request ) {
        var model = new CommandDefinition
        {
            Id = request.Id,
            Name = request.Name,
            RequestSchemaUri = request.RequestSchemaUri,
            ResponseSchemaUri = request.ResponseSchemaUri,
            TimeoutSeconds = request.TimeoutSeconds,
        };
        return model;
    }

}
