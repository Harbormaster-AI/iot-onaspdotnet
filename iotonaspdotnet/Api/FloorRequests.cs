namespace iotonaspdotnet.Api.Floor;

public record CreateFloorRequest(string FloorNumber, decimal Balance , Guid BuildingId);
public record UpdateFloorRequest(string FloorNumber, decimal Balance , Guid BuildingId);
public record FloorResponse(Guid Id, string FloorNumber, decimal Balance , Guid BuildingId);
