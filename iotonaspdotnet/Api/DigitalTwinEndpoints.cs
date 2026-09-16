using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class DigitalTwinEndpoints
{
    public static IEndpointRouteBuilder MapDigitalTwinEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/digitalTwin").WithTags("DigitalTwins");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IDigitalTwinService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IDigitalTwinService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateDigitalTwinRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DigitalTwin
        {
            Id = Guid.NewGuid(),

                TwinId = request.TwinId,
                DesiredStateVersion = request.DesiredStateVersion,
                ReportedStateVersion = request.ReportedStateVersion,
                LastSyncAt = request.LastSyncAt,

                IoTDeviceId = request.IoTDeviceId,
                GatewayId = request.GatewayId,
                TwinTemplateId = request.TwinTemplateId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/lowercaseClassNames/{lowercaseClassName.Id}", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateDigitalTwinRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DigitalTwin
        {
            Id = id,
            DigitalTwinNumber = request.DigitalTwinNumber,
            Balance = request.Balance,
            CustomerId = request.CustomerId
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
        IDigitalTwinService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static DigitalTwinResponse ToResponse(DigitalTwin lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.DigitalTwinNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
