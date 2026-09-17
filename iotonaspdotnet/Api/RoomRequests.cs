namespace iotonaspdotnet.Api;

, String
, Guid FloorId
public record CreateRoomRequest(string RoomNumber, decimal Balance, Guid FloorId);
public record UpdateRoomRequest(string RoomNumber, decimal Balance, Guid FloorId);
public record RoomResponse(Guid Id, string RoomNumber, decimal Balance, Guid FloorId);
