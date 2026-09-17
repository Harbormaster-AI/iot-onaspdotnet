using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class TwinChangeEventEndpoints
{
    public static IEndpointRouteBuilder MapTwinChangeEventEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/twinChangeEvent").WithTags("TwinChangeEvents");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ITwinChangeEventService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ITwinChangeEventService service,
        CancellationToken cancellationToken)
    {
        var twinChangeEvent = await service.GetByIdAsync(id, cancellationToken);
        return twinChangeEvent is null ? Results.NotFound() : Results.Ok(ToResponse( twinChangeEvent ));
    }

    private static async Task<IResult> Create(
        CreateTwinChangeEventRequest request,
        ITwinChangeEventService service,
        CancellationToken cancellationToken)
    {
        var twinChangeEvent = new TwinChangeEvent
        {
            Id = Guid.NewGuid(),

                EventId = request.EventId,
                OccurredAt = request.OccurredAt,
                ChangeType = request.ChangeType,

                DigitalTwinId = request.DigitalTwinId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/twinChangeEvents/twinChangeEvent.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateTwinChangeEventRequest request,
        ITwinChangeEventService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TwinChangeEvent
        {
            Id = id,
            EventId = request.EventId,
            OccurredAt = request.OccurredAt,
            ChangeType = request.ChangeType,

            DigitalTwinId = request.DigitalTwinId,
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
        ITwinChangeEventService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static TwinChangeEventResponse ToResponse(TwinChangeEvent lowercaseClassName)
        => new( twinChangeEvent.Id,
                , String, DateTime, TwinChangeType
                , DigitalTwinId );

}
