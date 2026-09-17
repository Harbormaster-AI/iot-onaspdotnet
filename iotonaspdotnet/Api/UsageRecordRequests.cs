namespace iotonaspdotnet.Api;

, Date, Date, Integer, Integer
, Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId
public record CreateUsageRecordRequest(string UsageRecordNumber, decimal Balance, Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId);
public record UpdateUsageRecordRequest(string UsageRecordNumber, decimal Balance, Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId);
public record UsageRecordResponse(Guid Id, string UsageRecordNumber, decimal Balance, Guid TenantId, Guid IoTDeviceId, Guid ConnectivityPlanId);
