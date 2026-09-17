namespace iotonaspdotnet.Api;


public record CreateHardwareModuleRequest( , String, Uri_, ModuleType , Guid DeviceVendorId );
public record UpdateHardwareModuleRequest( , String, Uri_, ModuleType , Guid DeviceVendorId );
public record HardwareModuleResponse( Guid Id, , String, Uri_, ModuleType , Guid DeviceVendorId );
