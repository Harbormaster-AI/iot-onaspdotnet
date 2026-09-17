using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignSite);
        group.MapDelete("/", unassignSite);
        group.MapDelete("/", assignRoom);
        group.MapDelete("/", unassignRoom);
        group.MapDelete("/", assignDigitalTwin);
        group.MapDelete("/", unassignDigitalTwin);

    group.MapDelete("/", addToDevices);
    group.MapDelete("/", removeFromDevices);

    group.MapDelete("/", addToEdgeApplications);
    group.MapDelete("/", removeFromEdgeApplications);

    group.MapDelete("/", addToCertificates);
    group.MapDelete("/", removeFromCertificates);

    group.MapDelete("/", addToNetworkProfiles);
    group.MapDelete("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        GatewayRequest request,
        IGatewayService service,
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
        GatewayRequest request,
        IGatewayService service,
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
        IGatewayService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( GatewayResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IGatewayService service,
        CancellationToken cancellationToken) {

        var gateway = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return gateway is null ? Results.NotFound() : Results.Ok( gateway );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSiteAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSiteAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignRoomAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignRoomAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDigitalTwinAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
    AssociationRequest request,
    IGatewayService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDigitalTwinAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDevicesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDevicesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.id,
            SoftwareVersion = request.SoftwareVersion;
            Site = request.Site;
            Room = request.Room;
            Devices = request.Devices;
            EdgeApplications = request.EdgeApplications;
            Certificates = request.Certificates;
            DigitalTwin = request.DigitalTwin;
            NetworkProfiles = request.NetworkProfiles;
            Status = request.Status;
        }
        return model;
    }
    private static async Task<IResult> AssignEdgeApplications(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToEdgeApplicationsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEdgeApplications(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromEdgeApplicationsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.id,
            SoftwareVersion = request.SoftwareVersion;
            Site = request.Site;
            Room = request.Room;
            Devices = request.Devices;
            EdgeApplications = request.EdgeApplications;
            Certificates = request.Certificates;
            DigitalTwin = request.DigitalTwin;
            NetworkProfiles = request.NetworkProfiles;
            Status = request.Status;
        }
        return model;
    }
    private static async Task<IResult> AssignCertificates(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCertificatesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCertificates(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCertificatesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.id,
            SoftwareVersion = request.SoftwareVersion;
            Site = request.Site;
            Room = request.Room;
            Devices = request.Devices;
            EdgeApplications = request.EdgeApplications;
            Certificates = request.Certificates;
            DigitalTwin = request.DigitalTwin;
            NetworkProfiles = request.NetworkProfiles;
            Status = request.Status;
        }
        return model;
    }
    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToNetworkProfilesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromNetworkProfilesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.id,
            SoftwareVersion = request.SoftwareVersion;
            Site = request.Site;
            Room = request.Room;
            Devices = request.Devices;
            EdgeApplications = request.EdgeApplications;
            Certificates = request.Certificates;
            DigitalTwin = request.DigitalTwin;
            NetworkProfiles = request.NetworkProfiles;
            Status = request.Status;
        }
        return model;
    }
}
