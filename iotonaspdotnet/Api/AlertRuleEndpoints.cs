using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToStreams);
    group.MapDelete("/", removeFromStreams);

    group.MapDelete("/", addToAlerts);
    group.MapDelete("/", removeFromAlerts);


        return app;
    }

    private static async Task<IResult> Create(
        AlertRuleRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
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
            var updated = await service.UpdateAsync(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetAll(
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( AlertRuleResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IAlertRuleService service,
        CancellationToken cancellationToken) {

        var alertRule = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return alertRule is null ? Results.NotFound() : Results.Ok( alertRule );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IAlertRuleService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@58458e00 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@58458e00( com.harbormaster.codetemplate.model.classes.ClassObject@58458e00Request request ) {
        var model = new AlertRule
        {
            Id = request.id,
        Name = request.Name
        Expression = request.Expression
        Tenant = request.Tenant
        Streams = request.Streams
        Alerts = request.Alerts
        Severity = request.Severity
        }
        return model;
    }
    private static async Task<IResult> AssignAlerts(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToAlertsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAlerts(
        AssociationRequest request,
        IAlertRuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromAlertsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@58458e00 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@58458e00( com.harbormaster.codetemplate.model.classes.ClassObject@58458e00Request request ) {
        var model = new AlertRule
        {
            Id = request.id,
        Name = request.Name
        Expression = request.Expression
        Tenant = request.Tenant
        Streams = request.Streams
        Alerts = request.Alerts
        Severity = request.Severity
        }
        return model;
    }
}
