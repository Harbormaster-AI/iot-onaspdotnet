using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class ActuatorInstanceEndpoints
{
    public static IEndpointRouteBuilder MapActuatorInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/actuatorInstance").WithTags("ActuatorInstances");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IActuatorInstanceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IActuatorInstanceService service,
        CancellationToken cancellationToken)
    {
        var actuatorInstance = await service.GetByIdAsync(id, cancellationToken);
        return actuatorInstance is null ? Results.NotFound() : Results.Ok(ToResponse( actuatorInstance ));
    }

    private static async Task<IResult> Create(
        CreateActuatorInstanceRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken)
    {
        var actuatorInstance = new ActuatorInstance
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                CommandTopic = request.CommandTopic,
                ActuatorType = request.ActuatorType,

                IoTDeviceId = request.IoTDeviceId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/actuatorInstances/actuatorInstance.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateActuatorInstanceRequest request,
        IActuatorInstanceService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ActuatorInstance
        {
            Id = id,
            Name = request.Name,
            CommandTopic = request.CommandTopic,
            ActuatorType = request.ActuatorType,

            IoTDeviceId = request.IoTDeviceId,
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
        IActuatorInstanceService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static ActuatorInstanceResponse ToResponse(ActuatorInstance lowercaseClassName)
        => new( actuatorInstance.Id,
                , String, TopicName, ActuatorType
                , IoTDeviceId );

}
