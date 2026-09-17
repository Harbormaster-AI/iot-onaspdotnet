namespace iotonaspdotnet.Api;

, String
, Guid SiteId
public record CreateBuildingRequest(string BuildingNumber, decimal Balance, Guid SiteId);
public record UpdateBuildingRequest(string BuildingNumber, decimal Balance, Guid SiteId);
public record BuildingResponse(Guid Id, string BuildingNumber, decimal Balance, Guid SiteId);
