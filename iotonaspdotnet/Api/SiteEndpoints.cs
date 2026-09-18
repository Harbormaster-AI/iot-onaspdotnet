using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);

    group.MapPut("/", addToBuildings);
    group.MapPut("/", removeFromBuildings);

    group.MapPut("/", addToDevices);
    group.MapPut("/", removeFromDevices);

    group.MapPut("/", addToGateways);
    group.MapPut("/", removeFromGateways);


        return app;
    }

    private static async Task<IResult> Create(
        SiteRequest request,
        ISiteService service,
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
        SiteRequest request,
        ISiteService service,
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
        ISiteService service,
        CancellationToken cancellationToken) {

        var site = await service.Get(identifier, cancellationToken);
        return site is null ? Results.NotFound() : Results.Ok( site );
    }


    private static async Task<IResult> GetAll(
        ISiteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SiteResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISiteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    ISiteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBuildings(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBuildings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBuildings(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBuildings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGateways(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGateways(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGateways(
        MultipleAssociationRequest request,
        ISiteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGateways(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Site mapRequestToSite( SiteRequest request ) {
        var model = new Site
        {
            Id = request.id,
            Name = request.Name,
            Address = request.Address,
            Timezone = request.Timezone,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
        return model;
    }

}
