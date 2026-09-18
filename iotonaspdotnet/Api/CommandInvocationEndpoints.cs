using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class CommandInvocationEndpoints
{
    public static IEndpointRouteBuilder MapCommandInvocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandInvocation").WithTags("CommandInvocations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDevice", AssignDevice);
        group.MapPut("/unassignDevice", UnassignDevice);
        group.MapPut("/assignCommandDefinition", AssignCommandDefinition);
        group.MapPut("/unassignCommandDefinition", UnassignCommandDefinition);
        group.MapPut("/assignActuator", AssignActuator);
        group.MapPut("/unassignActuator", UnassignActuator);
        group.MapPut("/assignUser", AssignUser);
        group.MapPut("/unassignUser", UnassignUser);


        return app;
    }

    private static async Task<IResult> Create(
        CommandInvocationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCommandInvocation( request );

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
        CommandInvocationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCommandInvocation( request );

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


    private static CommandInvocation mapRequestToCommandInvocation( CommandInvocationRequest request ) {
        var model = new CommandInvocation
        {
            Id = request.Id,
            InvocationId = request.InvocationId,
            RequestedAt = request.RequestedAt,
            CompletedAt = request.CompletedAt,
            Status = request.Status,
        };
        return model;
    }

}
