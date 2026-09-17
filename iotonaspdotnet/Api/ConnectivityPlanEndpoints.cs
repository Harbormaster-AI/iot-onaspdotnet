using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class ConnectivityPlanEndpoints
{
    public static IEndpointRouteBuilder MapConnectivityPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/connectivityPlan").WithTags("ConnectivityPlans");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToSimCards);
    group.MapDelete("/", removeFromSimCards);


        return app;
    }

    private static async Task<IResult> Create(
        ConnectivityPlanRequest request,
        IConnectivityPlanService service,
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
        ConnectivityPlanRequest request,
        IConnectivityPlanService service,
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
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( ConnectivityPlanResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var connectivityPlan = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return connectivityPlan is null ? Results.NotFound() : Results.Ok( connectivityPlan );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IConnectivityPlanService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignSimCards(
        AssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToSimCardsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSimCards(
        AssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromSimCardsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@33465070 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@33465070( com.harbormaster.codetemplate.model.classes.ClassObject@33465070Request request ) {
        var model = new ConnectivityPlan
        {
            Id = request.id,
        Name = request.Name
        DataCapMB = request.DataCapMB
        BillingCycleDays = request.BillingCycleDays
        SimCards = request.SimCards
        Tenant = request.Tenant
        }
        return model;
    }
}
