namespace iotonaspdotnet.Api.IoTDevice;

public record CreateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance , , , , , , , Guid DeviceModelIdGuid TenantIdGuid SiteIdGuid RoomIdGuid GatewayIdGuid DigitalTwinIdGuid ProvisioningRecordId);
public record UpdateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance , , , , , , , Guid DeviceModelIdGuid TenantIdGuid SiteIdGuid RoomIdGuid GatewayIdGuid DigitalTwinIdGuid ProvisioningRecordId);
public record IoTDeviceResponse(Guid Id, string IoTDeviceNumber, decimal Balance , , , , , , , Guid DeviceModelIdGuid TenantIdGuid SiteIdGuid RoomIdGuid GatewayIdGuid DigitalTwinIdGuid ProvisioningRecordId);
