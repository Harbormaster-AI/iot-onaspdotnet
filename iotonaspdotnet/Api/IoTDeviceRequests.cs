namespace iotonaspdotnet.Api.IoTDevice;

public record CreateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
public record UpdateIoTDeviceRequest(string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
public record IoTDeviceResponse(Guid Id, string IoTDeviceNumber, decimal Balance, Guid DeviceModelId, Guid TenantId, Guid SiteId, Guid RoomId, Guid GatewayId, Guid DigitalTwinId, Guid ProvisioningRecordId);
