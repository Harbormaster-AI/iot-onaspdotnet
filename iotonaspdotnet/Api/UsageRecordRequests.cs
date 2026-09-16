namespace iotonaspdotnet.Api.UsageRecord;

public record CreateUsageRecordRequest(string UsageRecordNumber, decimal Balance , , , Guid TenantIdGuid IoTDeviceIdGuid ConnectivityPlanId);
public record UpdateUsageRecordRequest(string UsageRecordNumber, decimal Balance , , , Guid TenantIdGuid IoTDeviceIdGuid ConnectivityPlanId);
public record UsageRecordResponse(Guid Id, string UsageRecordNumber, decimal Balance , , , Guid TenantIdGuid IoTDeviceIdGuid ConnectivityPlanId);
