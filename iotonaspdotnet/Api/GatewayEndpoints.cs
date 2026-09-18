using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class GatewayEndpoints
{
    public static IEndpointRouteBuilder MapGatewayEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/gateway").WithTags("Gateways");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignSite);
        group.MapPut("/", unassignSite);
        group.MapPut("/", assignRoom);
        group.MapPut("/", unassignRoom);
        group.MapPut("/", assignDigitalTwin);
        group.MapPut("/", unassignDigitalTwin);

    group.MapPut("/", addToDevices);
    group.MapPut("/", removeFromDevices);

    group.MapPut("/", addToEdgeApplications);
    group.MapPut("/", removeFromEdgeApplications);

    group.MapPut("/", addToCertificates);
    group.MapPut("/", removeFromCertificates);

    group.MapPut("/", addToNetworkProfiles);
    group.MapPut("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        GatewayRequest request,
        IGatewayService service,
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
        GatewayRequest request,
        IGatewayService service,
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
        IGatewayService service,
        CancellationToken cancellationToken) {

        var gateway = await service.Get(identifier, cancellationToken);
        return gateway is null ? Results.NotFound() : Results.Ok( gateway );
    }


    private static async Task<IResult> GetAll(
        IGatewayService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GatewayResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSite(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSite(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSite(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRoom(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRoom(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRoom(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDigitalTwin(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDigitalTwin(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDigitalTwin(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEdgeApplications(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEdgeApplications(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEdgeApplications(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEdgeApplications(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCertificates(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCertificates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCertificates(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCertificates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToNetworkProfiles(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToNetworkProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromNetworkProfiles(
        MultipleAssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromNetworkProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.id,
            SoftwareVersion = request.SoftwareVersion,
            Site = request.Site,
            Room = request.Room,
            Devices = request.Devices,
            EdgeApplications = request.EdgeApplications,
            Certificates = request.Certificates,
            DigitalTwin = request.DigitalTwin,
            NetworkProfiles = request.NetworkProfiles,
            Status = request.Status,
        };
        return model;
    }

}
