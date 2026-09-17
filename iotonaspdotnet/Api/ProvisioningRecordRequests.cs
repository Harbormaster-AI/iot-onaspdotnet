namespace iotonaspdotnet.Api;

, DateTime, String, ProvisioningMethod, ProvisioningStatus
, Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId
public record CreateProvisioningRecordRequest(string ProvisioningRecordNumber, decimal Balance, Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId);
public record UpdateProvisioningRecordRequest(string ProvisioningRecordNumber, decimal Balance, Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId);
public record ProvisioningRecordResponse(Guid Id, string ProvisioningRecordNumber, decimal Balance, Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId);
