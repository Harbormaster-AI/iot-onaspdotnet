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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignCommandDefinition);
        group.MapDelete("/", unassignCommandDefinition);
        group.MapDelete("/", assignActuator);
        group.MapDelete("/", unassignActuator);
        group.MapDelete("/", assignUser);
        group.MapDelete("/", unassignUser);


        return app;
    }

    private static async Task<IResult> Create(
        CommandInvocationRequest request,
        ICommandInvocationService service,
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
        CommandInvocationRequest request,
        ICommandInvocationService service,
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
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( CommandInvocationResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var commandInvocation = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return commandInvocation is null ? Results.NotFound() : Results.Ok( commandInvocation );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandDefinition(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignCommandDefinitionAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandDefinition(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignCommandDefinitionAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignActuator(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignActuatorAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignActuator(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignActuatorAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUser(
        AssociationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignUserAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUser(
    AssociationRequest request,
    ICommandInvocationService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignUserAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
