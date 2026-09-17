namespace iotonaspdotnet.Api;


public record CreateSiteRequest( , String, Address, String, Decimal, Decimal , Guid TenantId );
public record UpdateSiteRequest( , String, Address, String, Decimal, Decimal , Guid TenantId );
public record SiteResponse( Guid Id, , String, Address, String, Decimal, Decimal , Guid TenantId );
