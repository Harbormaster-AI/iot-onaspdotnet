namespace iotonaspdotnet.Api;


public record CreateDeviceGroupRequest( , String, String , Guid TenantId );
public record UpdateDeviceGroupRequest( , String, String , Guid TenantId );
public record DeviceGroupResponse( Guid Id, , String, String , Guid TenantId );
