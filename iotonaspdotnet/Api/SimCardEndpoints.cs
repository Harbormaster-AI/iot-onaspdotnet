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

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);
        group.MapDelete("/", assignConnectivityPlan);
        group.MapDelete("/", unassignConnectivityPlan);

    group.MapDelete("/", addToNetworkProfiles);
    group.MapDelete("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        SimCardRequest request,
        ISimCardService service,
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
        SimCardRequest request,
        ISimCardService service,
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
        ISimCardService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( SimCardResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISimCardService service,
        CancellationToken cancellationToken) {

        var simCard = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return simCard is null ? Results.NotFound() : Results.Ok( simCard );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    ISimCardService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignConnectivityPlanAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
    AssociationRequest request,
    ISimCardService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignConnectivityPlanAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToNetworkProfilesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        ISimCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromNetworkProfilesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@359181ff mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@359181ff( com.harbormaster.codetemplate.model.classes.ClassObject@359181ffRequest request ) {
        var model = new SimCard
        {
            Id = request.id,
        Iccid = request.Iccid
        Imsi = request.Imsi
        Carrier = request.Carrier
        NetworkProfiles = request.NetworkProfiles
        Tenant = request.Tenant
        ConnectivityPlan = request.ConnectivityPlan
        Status = request.Status
        }
        return model;
    }
}
