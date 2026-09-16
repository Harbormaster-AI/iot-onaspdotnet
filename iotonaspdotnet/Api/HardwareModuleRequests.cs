namespace iotonaspdotnet.Api.HardwareModule;

public record CreateHardwareModuleRequest(string HardwareModuleNumber, decimal Balance , Guid DeviceVendorId);
public record UpdateHardwareModuleRequest(string HardwareModuleNumber, decimal Balance , Guid DeviceVendorId);
public record HardwareModuleResponse(Guid Id, string HardwareModuleNumber, decimal Balance , Guid DeviceVendorId);
