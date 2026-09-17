using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class CommandDefinitionEndpoints
{
    public static IEndpointRouteBuilder MapCommandDefinitionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/commandDefinition").WithTags("CommandDefinitions");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ICommandDefinitionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ICommandDefinitionService service,
        CancellationToken cancellationToken)
    {
        var commandDefinition = await service.GetByIdAsync(id, cancellationToken);
        return commandDefinition is null ? Results.NotFound() : Results.Ok(ToResponse( commandDefinition ));
    }

    private static async Task<IResult> Create(
        CreateCommandDefinitionRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                RequestSchemaUri = request.RequestSchemaUri,
                ResponseSchemaUri = request.ResponseSchemaUri,
                TimeoutSeconds = request.TimeoutSeconds,

                DeviceModelId = request.DeviceModelId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/commandDefinitions/commandDefinition.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateCommandDefinitionRequest request,
        ICommandDefinitionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new CommandDefinition
        {
            Id = id,
            Name = request.Name,
            RequestSchemaUri = request.RequestSchemaUri,
            ResponseSchemaUri = request.ResponseSchemaUri,
            TimeoutSeconds = request.TimeoutSeconds,

            DeviceModelId = request.DeviceModelId,
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
        ICommandDefinitionService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static CommandDefinitionResponse ToResponse(CommandDefinition lowercaseClassName)
        => new( commandDefinition.Id,
                , String, Uri_, Uri_, Integer
                , DeviceModelId );

}
