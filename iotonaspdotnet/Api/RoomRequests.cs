namespace iotonaspdotnet.Api;


public record CreateRoomRequest( , String , Guid FloorId );
public record UpdateRoomRequest( , String , Guid FloorId );
public record RoomResponse( Guid Id, , String , Guid FloorId );
