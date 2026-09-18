using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);

    group.MapPut("/", addToSimCards);
    group.MapPut("/", removeFromSimCards);


        return app;
    }

    private static async Task<IResult> Create(
        ConnectivityPlanRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(lowercaseClassName, cancellationToken);
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
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var connectivityPlan = await service.Get(identifier, cancellationToken);
        return connectivityPlan is null ? Results.NotFound() : Results.Ok( connectivityPlan );
    }


    private static async Task<IResult> GetAll(
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConnectivityPlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IConnectivityPlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSimCards(
        MultipleAssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSimCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSimCards(
        MultipleAssociationRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSimCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private ConnectivityPlan mapRequestToConnectivityPlan( ConnectivityPlanRequest request ) {
        var model = new ConnectivityPlan
        {
            Id = request.id,
            Name = request.Name,
            DataCapMB = request.DataCapMB,
            BillingCycleDays = request.BillingCycleDays,
            SimCards = request.SimCards,
            Tenant = request.Tenant,
        };
        return model;
    }

}
