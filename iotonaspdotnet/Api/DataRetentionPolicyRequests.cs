namespace iotonaspdotnet.Api;


public record CreateDataRetentionPolicyRequest( , String, Integer , Guid TenantId );
public record UpdateDataRetentionPolicyRequest( , String, Integer , Guid TenantId );
public record DataRetentionPolicyResponse( Guid Id, , String, Integer , Guid TenantId );
