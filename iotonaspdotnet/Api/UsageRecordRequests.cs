namespace iotonaspdotnet.Api;


public record CreateUsageRecordRequest( , Date, Date, Integer, Integer , Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId );
public record UpdateUsageRecordRequest( , Date, Date, Integer, Integer , Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId );
public record UsageRecordResponse( Guid Id, , Date, Date, Integer, Integer , Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId );
