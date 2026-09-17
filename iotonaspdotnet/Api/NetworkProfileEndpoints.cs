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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignGateway);
        group.MapDelete("/", unassignGateway);
        group.MapDelete("/", assignSimCard);
        group.MapDelete("/", unassignSimCard);


        return app;
    }

    private static async Task<IResult> Create(
        NetworkProfileRequest request,
        INetworkProfileService service,
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
        NetworkProfileRequest request,
        INetworkProfileService service,
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
        INetworkProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( NetworkProfileResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        INetworkProfileService service,
        CancellationToken cancellationToken) {

        var networkProfile = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return networkProfile is null ? Results.NotFound() : Results.Ok( networkProfile );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSimCard(
        AssociationRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSimCardAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSimCard(
    AssociationRequest request,
    INetworkProfileService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSimCardAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
