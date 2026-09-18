using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class GatewayEndpoints
{
    public static IEndpointRouteBuilder MapGatewayEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/gateway").WithTags("Gateways");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSite", AssignSite);
        group.MapPut("/unassignSite", UnassignSite);
        group.MapPut("/assignRoom", AssignRoom);
        group.MapPut("/unassignRoom", UnassignRoom);
        group.MapPut("/assignDigitalTwin", AssignDigitalTwin);
        group.MapPut("/unassignDigitalTwin", UnassignDigitalTwin);

    group.MapPut("/addToDevices", AddToDevices);
    group.MapPut("/removeFromDevices", RemoveFromDevices);

    group.MapPut("/addToEdgeApplications", AddToEdgeApplications);
    group.MapPut("/removeFromEdgeApplications", RemoveFromEdgeApplications);

    group.MapPut("/addToCertificates", AddToCertificates);
    group.MapPut("/removeFromCertificates", RemoveFromCertificates);

    group.MapPut("/addToNetworkProfiles", AddToNetworkProfiles);
    group.MapPut("/removeFromNetworkProfiles", RemoveFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        GatewayRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGateway( request );

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
        GatewayRequest request,
        IGatewayService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGateway( request );

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
    private static Gateway mapRequestToGateway( GatewayRequest request ) {
        var model = new Gateway
        {
            Id = request.Id,
            SoftwareVersion = request.SoftwareVersion,
            Status = request.Status,
        };
        return model;
    }

}
