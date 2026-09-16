using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class TelemetrySchemaEndpoints
{
    public static IEndpointRouteBuilder MapTelemetrySchemaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetrySchema").WithTags("TelemetrySchemas");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ITelemetrySchemaService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateTelemetrySchemaRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TelemetrySchema
        {
            Id = Guid.NewGuid(),

                SchemaId = request.SchemaId,
                SchemaUri = request.SchemaUri,
                Encoding = request.Encoding,


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
        UpdateTelemetrySchemaRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TelemetrySchema
        {
            Id = id,
            TelemetrySchemaNumber = request.TelemetrySchemaNumber,
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
        ITelemetrySchemaService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static TelemetrySchemaResponse ToResponse(TelemetrySchema lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.TelemetrySchemaNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
