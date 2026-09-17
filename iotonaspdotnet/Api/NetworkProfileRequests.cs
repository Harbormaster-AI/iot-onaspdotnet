namespace iotonaspdotnet.Api;

public record CreateNetworkProfileRequest(string NetworkProfileNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid SimCardId);
public record UpdateNetworkProfileRequest(string NetworkProfileNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid SimCardId);
public record NetworkProfileResponse(Guid Id, string NetworkProfileNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid SimCardId);
