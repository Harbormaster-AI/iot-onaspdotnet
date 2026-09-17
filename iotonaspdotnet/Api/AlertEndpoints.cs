using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class AlertEndpoints
{
    public static IEndpointRouteBuilder MapAlertEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/alert").WithTags("Alerts");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IAlertService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IAlertService service,
        CancellationToken cancellationToken)
    {
        var alert = await service.GetByIdAsync(id, cancellationToken);
        return alert is null ? Results.NotFound() : Results.Ok(ToResponse( alert ));
    }

    private static async Task<IResult> Create(
        CreateAlertRequest request,
        IAlertService service,
        CancellationToken cancellationToken)
    {
        var alert = new Alert
        {
            Id = Guid.NewGuid(),

                RaisedAt = request.RaisedAt,
                ClearedAt = request.ClearedAt,
                Message = request.Message,
                Status = request.Status,

                IoTDeviceId = request.IoTDeviceId,
                AlertRuleId = request.AlertRuleId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/alerts/alert.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateAlertRequest request,
        IAlertService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Alert
        {
            Id = id,
            RaisedAt = request.RaisedAt,
            ClearedAt = request.ClearedAt,
            Message = request.Message,
            Status = request.Status,

            IoTDeviceId = request.IoTDeviceId,
            AlertRuleId = request.AlertRuleId,
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
        IAlertService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static AlertResponse ToResponse(Alert lowercaseClassName)
        => new( alert.Id,
                , DateTime, DateTime, String, AlertStatus
                , IoTDeviceId, AlertRuleId );

}
