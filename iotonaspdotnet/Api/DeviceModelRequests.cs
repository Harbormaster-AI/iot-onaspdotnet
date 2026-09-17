namespace iotonaspdotnet.Api;


public record CreateDeviceModelRequest( , String, String, String, ConnectivityType, TelemetryEncoding , Guid DeviceVendorId, Guid TwinTemplateId );
public record UpdateDeviceModelRequest( , String, String, String, ConnectivityType, TelemetryEncoding , Guid DeviceVendorId, Guid TwinTemplateId );
public record DeviceModelResponse( Guid Id, , String, String, String, ConnectivityType, TelemetryEncoding , Guid DeviceVendorId, Guid TwinTemplateId );
