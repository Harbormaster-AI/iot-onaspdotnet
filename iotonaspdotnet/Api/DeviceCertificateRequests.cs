namespace iotonaspdotnet.Api;

public record CreateDeviceCertificateRequest(string DeviceCertificateNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId);
public record UpdateDeviceCertificateRequest(string DeviceCertificateNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId);
public record DeviceCertificateResponse(Guid Id, string DeviceCertificateNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId);
