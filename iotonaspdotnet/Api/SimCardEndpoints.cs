using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class SimCardEndpoints
{
    public static IEndpointRouteBuilder MapSimCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/simCard").WithTags("SimCards");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);
        group.MapPut("/", assignConnectivityPlan);
        group.MapPut("/", unassignConnectivityPlan);

    group.MapPut("/", addToNetworkProfiles);
    group.MapPut("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        SimCardRequest request,
        ISimCardService service,
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
        SimCardRequest request,
        ISimCardService service,
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
        ISimCardService service,
        CancellationToken cancellationToken) {

        var simCard = await service.Get(identifier, cancellationToken);
        return simCard is null ? Results.NotFound() : Results.Ok( simCard );
    }


    private static async Task<IResult> GetAll(
        ISimCardService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SimCardResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    ISimCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignConnectivityPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignConnectivityPlan(
    AssociationRequest request,
    ISimCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignConnectivityPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToNetworkProfiles(
        MultipleAssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToNetworkProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromNetworkProfiles(
        MultipleAssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromNetworkProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private SimCard mapRequestToSimCard( SimCardRequest request ) {
        var model = new SimCard
        {
            Id = request.id,
            Iccid = request.Iccid,
            Imsi = request.Imsi,
            Carrier = request.Carrier,
            NetworkProfiles = request.NetworkProfiles,
            Tenant = request.Tenant,
            ConnectivityPlan = request.ConnectivityPlan,
            Status = request.Status,
        };
        return model;
    }

}
