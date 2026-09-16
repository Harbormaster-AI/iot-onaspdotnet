namespace iotonaspdotnet.Api.NetworkProfile;

public record CreateNetworkProfileRequest(string NetworkProfileNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid SimCardId);
public record UpdateNetworkProfileRequest(string NetworkProfileNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid SimCardId);
public record NetworkProfileResponse(Guid Id, string NetworkProfileNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid SimCardId);
