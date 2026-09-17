using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class AlertRuleEndpoints
{
    public static IEndpointRouteBuilder MapAlertRuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/alertRule").WithTags("AlertRules");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IAlertRuleService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IAlertRuleService service,
        CancellationToken cancellationToken)
    {
        var alertRule = await service.GetByIdAsync(id, cancellationToken);
        return alertRule is null ? Results.NotFound() : Results.Ok(ToResponse( alertRule ));
    }

    private static async Task<IResult> Create(
        CreateAlertRuleRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken)
    {
        var alertRule = new AlertRule
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                Expression = request.Expression,
                Severity = request.Severity,

                TenantId = request.TenantId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/alertRules/alertRule.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateAlertRuleRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new AlertRule
        {
            Id = id,
            Name = request.Name,
            Expression = request.Expression,
            Severity = request.Severity,

            TenantId = request.TenantId,
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
        IAlertRuleService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static AlertRuleResponse ToResponse(AlertRule lowercaseClassName)
        => new( alertRule.Id,
                , String, String, AlertSeverity
                , TenantId );

}
