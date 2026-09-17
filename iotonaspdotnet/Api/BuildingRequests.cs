namespace iotonaspdotnet.Api;


public record CreateBuildingRequest( , String , Guid SiteId );
public record UpdateBuildingRequest( , String , Guid SiteId );
public record BuildingResponse( Guid Id, , String , Guid SiteId );
