using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class SiteEndpoints
{
    public static IEndpointRouteBuilder MapSiteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/site").WithTags("Sites");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToBuildings);
    group.MapDelete("/", removeFromBuildings);

    group.MapDelete("/", addToDevices);
    group.MapDelete("/", removeFromDevices);

    group.MapDelete("/", addToGateways);
    group.MapDelete("/", removeFromGateways);


        return app;
    }

    private static async Task<IResult> Create(
        SiteRequest request,
        ISiteService service,
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
        SiteRequest request,
        ISiteService service,
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
        ISiteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( SiteResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISiteService service,
        CancellationToken cancellationToken) {

        var site = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return site is null ? Results.NotFound() : Results.Ok( site );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    ISiteService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignBuildings(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToBuildingsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBuildings(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromBuildingsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Site mapRequestToSite( SiteRequest request ) {
        var model = new Site
        {
            Id = request.id,
            Name = request.Name;
            Address = request.Address;
            Timezone = request.Timezone;
            Latitude = request.Latitude;
            Longitude = request.Longitude;
            Tenant = request.Tenant;
            Buildings = request.Buildings;
            Devices = request.Devices;
            Gateways = request.Gateways;
        }
        return model;
    }
    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDevicesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDevicesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Site mapRequestToSite( SiteRequest request ) {
        var model = new Site
        {
            Id = request.id,
            Name = request.Name;
            Address = request.Address;
            Timezone = request.Timezone;
            Latitude = request.Latitude;
            Longitude = request.Longitude;
            Tenant = request.Tenant;
            Buildings = request.Buildings;
            Devices = request.Devices;
            Gateways = request.Gateways;
        }
        return model;
    }
    private static async Task<IResult> AssignGateways(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToGatewaysAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateways(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromGatewaysAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Site mapRequestToSite( SiteRequest request ) {
        var model = new Site
        {
            Id = request.id,
            Name = request.Name;
            Address = request.Address;
            Timezone = request.Timezone;
            Latitude = request.Latitude;
            Longitude = request.Longitude;
            Tenant = request.Tenant;
            Buildings = request.Buildings;
            Devices = request.Devices;
            Gateways = request.Gateways;
        }
        return model;
    }
}
