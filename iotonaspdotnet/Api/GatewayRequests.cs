namespace iotonaspdotnet.Api;

, String, DeviceStatus
, Guid SiteId, Guid RoomId, Guid DigitalTwinId
public record CreateGatewayRequest(string GatewayNumber, decimal Balance, Guid SiteId, Guid RoomId, Guid DigitalTwinId);
public record UpdateGatewayRequest(string GatewayNumber, decimal Balance, Guid SiteId, Guid RoomId, Guid DigitalTwinId);
public record GatewayResponse(Guid Id, string GatewayNumber, decimal Balance, Guid SiteId, Guid RoomId, Guid DigitalTwinId);
