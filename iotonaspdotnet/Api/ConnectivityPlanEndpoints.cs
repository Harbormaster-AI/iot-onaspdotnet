
using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class ConnectivityPlanEndpoints
{
    public static IEndpointRouteBuilder MapConnectivityPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/connectivityPlan").WithTags("ConnectivityPlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignTenant", AssignTenant);
        group.MapPut("/unassignTenant", UnassignTenant);

    group.MapPut("/addToSimCards", AddToSimCards);
    group.MapPut("/removeFromSimCards", RemoveFromSimCards);


        return app;
    }

    private static async Task<IResult> Create(
        ConnectivityPlanRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConnectivityPlan( request );

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
        ConnectivityPlanRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConnectivityPlan( request );

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
    private static ConnectivityPlan mapRequestToConnectivityPlan( ConnectivityPlanRequest request ) {
        var model = new ConnectivityPlan
        {
            Id = request.Id,
            Name = request.Name,
            DataCapMB = request.DataCapMB,
            BillingCycleDays = request.BillingCycleDays,
        };
        return model;
    }

}
