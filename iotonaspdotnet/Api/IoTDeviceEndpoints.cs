using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class IoTDeviceEndpoints
{
    public static IEndpointRouteBuilder MapIoTDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/ioTDevice").WithTags("IoTDevices");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDeviceModel);
        group.MapPut("/", unassignDeviceModel);
        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);
        group.MapPut("/", assignSite);
        group.MapPut("/", unassignSite);
        group.MapPut("/", assignRoom);
        group.MapPut("/", unassignRoom);
        group.MapPut("/", assignGateway);
        group.MapPut("/", unassignGateway);
        group.MapPut("/", assignDigitalTwin);
        group.MapPut("/", unassignDigitalTwin);
        group.MapPut("/", assignProvisioningRecord);
        group.MapPut("/", unassignProvisioningRecord);

    group.MapPut("/", addToSensors);
    group.MapPut("/", removeFromSensors);

    group.MapPut("/", addToActuators);
    group.MapPut("/", removeFromActuators);

    group.MapPut("/", addToCertificates);
    group.MapPut("/", removeFromCertificates);

    group.MapPut("/", addToTelemetryStreams);
    group.MapPut("/", removeFromTelemetryStreams);

    group.MapPut("/", addToCommandInvocations);
    group.MapPut("/", removeFromCommandInvocations);

    group.MapPut("/", addToAlerts);
    group.MapPut("/", removeFromAlerts);

    group.MapPut("/", addToDeviceGroups);
    group.MapPut("/", removeFromDeviceGroups);

    group.MapPut("/", addToNetworkProfiles);
    group.MapPut("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        IoTDeviceRequest request,
        IIoTDeviceService service,
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
        IoTDeviceRequest request,
        IIoTDeviceService service,
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
        IIoTDeviceService service,
        CancellationToken cancellationToken) {

        var ioTDevice = await service.Get(identifier, cancellationToken);
        return ioTDevice is null ? Results.NotFound() : Results.Ok( ioTDevice );
    }


    private static async Task<IResult> GetAll(
        IIoTDeviceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( IoTDeviceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeviceModel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeviceModel(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeviceModel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSite(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSite(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSite(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRoom(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRoom(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRoom(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGateway(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGateway(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGateway(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDigitalTwin(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDigitalTwin(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDigitalTwin(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProvisioningRecord(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProvisioningRecord(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProvisioningRecord(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProvisioningRecord(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSensors(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSensors(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSensors(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSensors(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToActuators(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToActuators(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromActuators(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromActuators(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCertificates(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCertificates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCertificates(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCertificates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTelemetryStreams(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTelemetryStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTelemetryStreams(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTelemetryStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCommandInvocations(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCommandInvocations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCommandInvocations(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCommandInvocations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAlerts(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAlerts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAlerts(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAlerts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDeviceGroups(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeviceGroups(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeviceGroups(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeviceGroups(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToNetworkProfiles(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToNetworkProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromNetworkProfiles(
        MultipleAssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromNetworkProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static IoTDevice mapRequestToIoTDevice( IoTDeviceRequest request ) {
        var model = new IoTDevice
        {
            Id = request.Id,
            DeviceId = request.DeviceId,
            SerialNumber = request.SerialNumber,
            LastSeen = request.LastSeen,
            FirmwareVersion = request.FirmwareVersion,
            Status = request.Status,
            PowerSource = request.PowerSource,
        };
        return model;
    }

}
