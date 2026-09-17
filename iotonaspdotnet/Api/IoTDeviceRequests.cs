namespace iotonaspdotnet.Api;

, DeviceId, String, DateTime, FirmwareVersion, DeviceStatus, PowerSource
, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId
public record CreateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
public record UpdateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
public record IoTDeviceResponse(Guid Id, string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
