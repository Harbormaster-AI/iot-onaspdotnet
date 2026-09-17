namespace iotonaspdotnet.Api;


public record CreateApiKeyRequest( , String, String, DateTime, DateTime , Guid AccessPolicyId );
public record UpdateApiKeyRequest( , String, String, DateTime, DateTime , Guid AccessPolicyId );
public record ApiKeyResponse( Guid Id, , String, String, DateTime, DateTime , Guid AccessPolicyId );
