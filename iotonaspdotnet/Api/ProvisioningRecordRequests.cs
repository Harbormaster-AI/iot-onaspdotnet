namespace iotonaspdotnet.Api;


public record CreateProvisioningRecordRequest( , DateTime, String, ProvisioningMethod, ProvisioningStatus , Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId );
public record UpdateProvisioningRecordRequest( , DateTime, String, ProvisioningMethod, ProvisioningStatus , Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId );
public record ProvisioningRecordResponse( Guid Id, , DateTime, String, ProvisioningMethod, ProvisioningStatus , Guid IoTDeviceId, Guid DeviceCertificateId, Guid TenantId );
