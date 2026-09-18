using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class NetworkProfileEndpoints
{
    public static IEndpointRouteBuilder MapNetworkProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/networkProfile").WithTags("NetworkProfiles");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignGateway);
        group.MapPut("/", unassignGateway);
        group.MapPut("/", assignSimCard);
        group.MapPut("/", unassignSimCard);


        return app;
    }

    private static async Task<IResult> Create(
        NetworkProfileRequest request,
        INetworkProfileService service,
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
        NetworkProfileRequest request,
        INetworkProfileService service,
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
        INetworkProfileService service,
        CancellationToken cancellationToken) {

        var networkProfile = await service.Get(identifier, cancellationToken);
        return networkProfile is null ? Results.NotFound() : Results.Ok( networkProfile );
    }


    private static async Task<IResult> GetAll(
        INetworkProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( NetworkProfileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGateway(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGateway(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGateway(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSimCard(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSimCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSimCard(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSimCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private NetworkProfile mapRequestToNetworkProfile( NetworkProfileRequest request ) {
        var model = new NetworkProfile
        {
            Id = request.id,
            ProfileName = request.ProfileName,
            Ssid = request.Ssid,
            Apn = request.Apn,
            Device = request.Device,
            Gateway = request.Gateway,
            SimCard = request.SimCard,
            ConnectivityType = request.ConnectivityType,
        };
        return model;
    }

}
