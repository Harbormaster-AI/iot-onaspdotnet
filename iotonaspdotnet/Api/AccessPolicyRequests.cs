namespace iotonaspdotnet.Api;


public record CreateAccessPolicyRequest( , String, String, DateTime , Guid TenantId );
public record UpdateAccessPolicyRequest( , String, String, DateTime , Guid TenantId );
public record AccessPolicyResponse( Guid Id, , String, String, DateTime , Guid TenantId );
