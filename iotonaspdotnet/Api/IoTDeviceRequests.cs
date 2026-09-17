namespace iotonaspdotnet.Api;


public record CreateIoTDeviceRequest( , DeviceId, String, DateTime, FirmwareVersion, DeviceStatus, PowerSource , Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId );
public record UpdateIoTDeviceRequest( , DeviceId, String, DateTime, FirmwareVersion, DeviceStatus, PowerSource , Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId );
public record IoTDeviceResponse( Guid Id, , DeviceId, String, DateTime, FirmwareVersion, DeviceStatus, PowerSource , Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId );
