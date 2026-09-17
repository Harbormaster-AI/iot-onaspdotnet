namespace iotonaspdotnet.Api;

, String, Integer
, Guid BuildingId
public record CreateFloorRequest(string FloorNumber, decimal Balance, Guid BuildingId);
public record UpdateFloorRequest(string FloorNumber, decimal Balance, Guid BuildingId);
public record FloorResponse(Guid Id, string FloorNumber, decimal Balance, Guid BuildingId);
