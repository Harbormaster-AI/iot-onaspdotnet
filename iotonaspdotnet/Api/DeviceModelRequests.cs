namespace iotonaspdotnet.Api.DeviceModel;

public record CreateDeviceModelRequest(string DeviceModelNumber, decimal Balance , , Guid DeviceVendorIdGuid TwinTemplateId);
public record UpdateDeviceModelRequest(string DeviceModelNumber, decimal Balance , , Guid DeviceVendorIdGuid TwinTemplateId);
public record DeviceModelResponse(Guid Id, string DeviceModelNumber, decimal Balance , , Guid DeviceVendorIdGuid TwinTemplateId);
