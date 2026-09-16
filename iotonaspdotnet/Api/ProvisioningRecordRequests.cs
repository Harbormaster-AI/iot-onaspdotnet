namespace iotonaspdotnet.Api.ProvisioningRecord;

public record CreateProvisioningRecordRequest(string ProvisioningRecordNumber, decimal Balance , , , Guid IoTDeviceIdGuid DeviceCertificateIdGuid TenantId);
public record UpdateProvisioningRecordRequest(string ProvisioningRecordNumber, decimal Balance , , , Guid IoTDeviceIdGuid DeviceCertificateIdGuid TenantId);
public record ProvisioningRecordResponse(Guid Id, string ProvisioningRecordNumber, decimal Balance , , , Guid IoTDeviceIdGuid DeviceCertificateIdGuid TenantId);
