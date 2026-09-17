using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignDeviceModel);
        group.MapDelete("/", unassignDeviceModel);
        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);
        group.MapDelete("/", assignSite);
        group.MapDelete("/", unassignSite);
        group.MapDelete("/", assignRoom);
        group.MapDelete("/", unassignRoom);
        group.MapDelete("/", assignGateway);
        group.MapDelete("/", unassignGateway);
        group.MapDelete("/", assignDigitalTwin);
        group.MapDelete("/", unassignDigitalTwin);
        group.MapDelete("/", assignProvisioningRecord);
        group.MapDelete("/", unassignProvisioningRecord);

    group.MapDelete("/", addToSensors);
    group.MapDelete("/", removeFromSensors);

    group.MapDelete("/", addToActuators);
    group.MapDelete("/", removeFromActuators);

    group.MapDelete("/", addToCertificates);
    group.MapDelete("/", removeFromCertificates);

    group.MapDelete("/", addToTelemetryStreams);
    group.MapDelete("/", removeFromTelemetryStreams);

    group.MapDelete("/", addToCommandInvocations);
    group.MapDelete("/", removeFromCommandInvocations);

    group.MapDelete("/", addToAlerts);
    group.MapDelete("/", removeFromAlerts);

    group.MapDelete("/", addToDeviceGroups);
    group.MapDelete("/", removeFromDeviceGroups);

    group.MapDelete("/", addToNetworkProfiles);
    group.MapDelete("/", removeFromNetworkProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        IoTDeviceRequest request,
        IIoTDeviceService service,
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
        IoTDeviceRequest request,
        IIoTDeviceService service,
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
        IIoTDeviceService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( IoTDeviceResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {

        var ioTDevice = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return ioTDevice is null ? Results.NotFound() : Results.Ok( ioTDevice );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceModelAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceModelAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignSiteAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSite(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignSiteAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignRoomAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRoom(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignRoomAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDigitalTwinAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDigitalTwin(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDigitalTwinAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProvisioningRecord(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignProvisioningRecordAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProvisioningRecord(
    AssociationRequest request,
    IIoTDeviceService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignProvisioningRecordAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignSensors(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToSensorsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSensors(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromSensorsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignActuators(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToActuatorsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignActuators(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromActuatorsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignCertificates(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCertificatesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCertificates(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCertificatesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignTelemetryStreams(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToTelemetryStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTelemetryStreams(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromTelemetryStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCommandInvocationsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCommandInvocationsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignAlerts(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToAlertsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAlerts(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromAlertsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignDeviceGroups(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDeviceGroupsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceGroups(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDeviceGroupsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToNetworkProfilesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignNetworkProfiles(
        AssociationRequest request,
        IIoTDeviceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromNetworkProfilesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@3c0c008b( com.harbormaster.codetemplate.model.classes.ClassObject@3c0c008bRequest request ) {
        var model = new IoTDevice
        {
            Id = request.id,
        DeviceId = request.DeviceId
        SerialNumber = request.SerialNumber
        LastSeen = request.LastSeen
        FirmwareVersion = request.FirmwareVersion
        DeviceModel = request.DeviceModel
        Tenant = request.Tenant
        Site = request.Site
        Room = request.Room
        Gateway = request.Gateway
        Sensors = request.Sensors
        Actuators = request.Actuators
        Certificates = request.Certificates
        DigitalTwin = request.DigitalTwin
        TelemetryStreams = request.TelemetryStreams
        CommandInvocations = request.CommandInvocations
        Alerts = request.Alerts
        ProvisioningRecord = request.ProvisioningRecord
        DeviceGroups = request.DeviceGroups
        NetworkProfiles = request.NetworkProfiles
        Status = request.Status
        PowerSource = request.PowerSource
        }
        return model;
    }
}
