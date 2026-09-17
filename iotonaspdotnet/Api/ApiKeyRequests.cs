namespace iotonaspdotnet.Api;

, String, String, DateTime, DateTime
, Guid AccessPolicyId
public record CreateApiKeyRequest(string ApiKeyNumber, decimal Balance, Guid AccessPolicyId);
public record UpdateApiKeyRequest(string ApiKeyNumber, decimal Balance, Guid AccessPolicyId);
public record ApiKeyResponse(Guid Id, string ApiKeyNumber, decimal Balance, Guid AccessPolicyId);
