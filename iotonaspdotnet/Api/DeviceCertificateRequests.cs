namespace iotonaspdotnet.Api.DeviceCertificate;

public record CreateDeviceCertificateRequest(string DeviceCertificateNumber, decimal Balance , , Guid IoTDeviceIdGuid GatewayId);
public record UpdateDeviceCertificateRequest(string DeviceCertificateNumber, decimal Balance , , Guid IoTDeviceIdGuid GatewayId);
public record DeviceCertificateResponse(Guid Id, string DeviceCertificateNumber, decimal Balance , , Guid IoTDeviceIdGuid GatewayId);
