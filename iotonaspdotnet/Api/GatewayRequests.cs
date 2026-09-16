namespace iotonaspdotnet.Api.Gateway;

public record CreateGatewayRequest(string GatewayNumber, decimal Balance , , , Guid SiteIdGuid RoomIdGuid DigitalTwinId);
public record UpdateGatewayRequest(string GatewayNumber, decimal Balance , , , Guid SiteIdGuid RoomIdGuid DigitalTwinId);
public record GatewayResponse(Guid Id, string GatewayNumber, decimal Balance , , , Guid SiteIdGuid RoomIdGuid DigitalTwinId);
