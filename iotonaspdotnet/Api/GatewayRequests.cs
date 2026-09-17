namespace iotonaspdotnet.Api;


public record CreateGatewayRequest( , String, DeviceStatus , Guid SiteId, Guid RoomId, Guid DigitalTwinId );
public record UpdateGatewayRequest( , String, DeviceStatus , Guid SiteId, Guid RoomId, Guid DigitalTwinId );
public record GatewayResponse( Guid Id, , String, DeviceStatus , Guid SiteId, Guid RoomId, Guid DigitalTwinId );
