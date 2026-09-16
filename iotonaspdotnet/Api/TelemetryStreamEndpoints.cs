using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class TelemetryStreamEndpoints
{
    public static IEndpointRouteBuilder MapTelemetryStreamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetryStream").WithTags("TelemetryStreams");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ITelemetryStreamService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ITelemetryStreamService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateTelemetryStreamRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TelemetryStream
        {
            Id = Guid.NewGuid(),

                StreamName = request.StreamName,
                RetentionDays = request.RetentionDays,
                Qos = request.Qos,

                IoTDeviceId = request.IoTDeviceId,
                SensorInstanceId = request.SensorInstanceId,
                TelemetrySchemaId = request.TelemetrySchemaId,
                MessagingEndpointId = request.MessagingEndpointId,
                DataRetentionPolicyId = request.DataRetentionPolicyId,

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
        UpdateTelemetryStreamRequest request,
        ITelemetryStreamService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TelemetryStream
        {
            Id = id,
            TelemetryStreamNumber = request.TelemetryStreamNumber,
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
        ITelemetryStreamService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static TelemetryStreamResponse ToResponse(TelemetryStream lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.TelemetryStreamNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
