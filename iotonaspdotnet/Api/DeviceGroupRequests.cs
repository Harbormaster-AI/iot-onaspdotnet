namespace iotonaspdotnet.Api;

, String, String
, Guid TenantId
public record CreateDeviceGroupRequest(string DeviceGroupNumber, decimal Balance, Guid TenantId);
public record UpdateDeviceGroupRequest(string DeviceGroupNumber, decimal Balance, Guid TenantId);
public record DeviceGroupResponse(Guid Id, string DeviceGroupNumber, decimal Balance, Guid TenantId);
