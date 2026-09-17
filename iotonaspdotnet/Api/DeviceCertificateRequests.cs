namespace iotonaspdotnet.Api;


public record CreateDeviceCertificateRequest( , String, DateTime, DateTime, String, CertificateType , Guid IoTDeviceId, Guid GatewayId );
public record UpdateDeviceCertificateRequest( , String, DateTime, DateTime, String, CertificateType , Guid IoTDeviceId, Guid GatewayId );
public record DeviceCertificateResponse( Guid Id, , String, DateTime, DateTime, String, CertificateType , Guid IoTDeviceId, Guid GatewayId );
