using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class AlertRuleEndpoints
{
    public static IEndpointRouteBuilder MapAlertRuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/alertRule").WithTags("AlertRules");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);

    group.MapPut("/", addToStreams);
    group.MapPut("/", removeFromStreams);

    group.MapPut("/", addToAlerts);
    group.MapPut("/", removeFromAlerts);


        return app;
    }

    private static async Task<IResult> Create(
        AlertRuleRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

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
        AlertRuleRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

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
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var alertRule = await service.Get(identifier, cancellationToken);
        return alertRule is null ? Results.NotFound() : Results.Ok( alertRule );
    }


    private static async Task<IResult> GetAll(
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AlertRuleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IAlertRuleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToStreams(
        MultipleAssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromStreams(
        MultipleAssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAlerts(
        MultipleAssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAlerts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAlerts(
        MultipleAssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAlerts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static AlertRule mapRequestToAlertRule( AlertRuleRequest request ) {
        var model = new AlertRule
        {
            Id = request.Id,
            Name = request.Name,
            Expression = request.Expression,
            Severity = request.Severity,
        };
        return model;
    }

}
