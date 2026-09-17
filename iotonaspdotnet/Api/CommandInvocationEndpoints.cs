using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class CommandInvocationEndpoints
{
    public static IEndpointRouteBuilder MapCommandInvocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandInvocation").WithTags("CommandInvocations");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ICommandInvocationService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ICommandInvocationService service,
        CancellationToken cancellationToken)
    {
        var commandInvocation = await service.GetByIdAsync(id, cancellationToken);
        return commandInvocation is null ? Results.NotFound() : Results.Ok(ToResponse( commandInvocation ));
    }

    private static async Task<IResult> Create(
        CreateCommandInvocationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken)
    {
        var commandInvocation = new CommandInvocation
        {
            Id = Guid.NewGuid(),

                InvocationId = request.InvocationId,
                RequestedAt = request.RequestedAt,
                CompletedAt = request.CompletedAt,
                Status = request.Status,

                IoTDeviceId = request.IoTDeviceId,
                CommandDefinitionId = request.CommandDefinitionId,
                ActuatorInstanceId = request.ActuatorInstanceId,
                TenantUserId = request.TenantUserId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/commandInvocations/commandInvocation.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateCommandInvocationRequest request,
        ICommandInvocationService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new CommandInvocation
        {
            Id = id,
            InvocationId = request.InvocationId,
            RequestedAt = request.RequestedAt,
            CompletedAt = request.CompletedAt,
            Status = request.Status,

            IoTDeviceId = request.IoTDeviceId,
            CommandDefinitionId = request.CommandDefinitionId,
            ActuatorInstanceId = request.ActuatorInstanceId,
            TenantUserId = request.TenantUserId,
        };

        try
        {
            var updated = await service.UpdateAsync(lowercaseClassName, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> Delete(
        Guid id,
        ICommandInvocationService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static CommandInvocationResponse ToResponse(CommandInvocation lowercaseClassName)
        => new( commandInvocation.Id,
                , String, DateTime, DateTime, CommandStatus
                , IoTDeviceId, CommandDefinitionId, ActuatorInstanceId, TenantUserId );

}
