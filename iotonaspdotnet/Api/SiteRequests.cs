namespace iotonaspdotnet.Api;

public record CreateSiteRequest(string SiteNumber, decimal Balance, Guid TenantId);
public record UpdateSiteRequest(string SiteNumber, decimal Balance, Guid TenantId);
public record SiteResponse(Guid Id, string SiteNumber, decimal Balance, Guid TenantId);
