namespace iotonaspdotnet.Api;


public record CreateFloorRequest( , String, Integer , Guid BuildingId );
public record UpdateFloorRequest( , String, Integer , Guid BuildingId );
public record FloorResponse( Guid Id, , String, Integer , Guid BuildingId );
