namespace iotonaspdotnet.Api.DeviceGroup;

public record CreateDeviceGroupRequest(string DeviceGroupNumber, decimal Balance, Guid TenantId);
public record UpdateDeviceGroupRequest(string DeviceGroupNumber, decimal Balance, Guid TenantId);
public record DeviceGroupResponse(Guid Id, string DeviceGroupNumber, decimal Balance, Guid TenantId);
