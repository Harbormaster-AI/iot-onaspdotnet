namespace iotonaspdotnet.Api;

public record CreateDeviceModelRequest(string DeviceModelNumber, decimal Balance, Guid DeviceVendorId, Guid TwinTemplateId);
public record UpdateDeviceModelRequest(string DeviceModelNumber, decimal Balance, Guid DeviceVendorId, Guid TwinTemplateId);
public record DeviceModelResponse(Guid Id, string DeviceModelNumber, decimal Balance, Guid DeviceVendorId, Guid TwinTemplateId);
