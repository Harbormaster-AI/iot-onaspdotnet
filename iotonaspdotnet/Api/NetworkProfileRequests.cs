namespace iotonaspdotnet.Api;


public record CreateNetworkProfileRequest( , String, String, String, ConnectivityType , Guid IoTDeviceId, Guid GatewayId, Guid SimCardId );
public record UpdateNetworkProfileRequest( , String, String, String, ConnectivityType , Guid IoTDeviceId, Guid GatewayId, Guid SimCardId );
public record NetworkProfileResponse( Guid Id, , String, String, String, ConnectivityType , Guid IoTDeviceId, Guid GatewayId, Guid SimCardId );
