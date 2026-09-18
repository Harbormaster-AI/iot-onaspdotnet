using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class CommandInvocationEndpoints
{
    public static IEndpointRouteBuilder MapCommandInvocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandInvocation").WithTags("CommandInvocations");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignCommandDefinition);
        group.MapPut("/", unassignCommandDefinition);
        group.MapPut("/", assignActuator);
        group.MapPut("/", unassignActuator);
        group.MapPut("/", assignUser);
        group.MapPut("/", unassignUser);


        return app;
    }

    private static async Task<IResult> Create(
        CommandInvocationRequest request,
        ICommandInvocationService service,
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
        CommandInvocationRequest request,
        ICommandInvocationService service,
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
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var commandInvocation = await service.Get(identifier, cancellationToken);
        return commandInvocation is null ? Results.NotFound() : Results.Ok( commandInvocation );
    }


    private static async Task<IResult> GetAll(
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CommandInvocationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandDefinition(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCommandDefinition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCommandDefinition(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCommandDefinition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignActuator(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignActuator(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignActuator(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignActuator(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUser(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignUser(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignUser(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignUser(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private CommandInvocation mapRequestToCommandInvocation( CommandInvocationRequest request ) {
        var model = new CommandInvocation
        {
            Id = request.id,
            InvocationId = request.InvocationId,
            RequestedAt = request.RequestedAt,
            CompletedAt = request.CompletedAt,
            Device = request.Device,
            CommandDefinition = request.CommandDefinition,
            Actuator = request.Actuator,
            User = request.User,
            Status = request.Status,
        };
        return model;
    }

}
