namespace iotonaspdotnet.Api;

, String, String, DateTime
, Guid TenantId
public record CreateAccessPolicyRequest(string AccessPolicyNumber, decimal Balance, Guid TenantId);
public record UpdateAccessPolicyRequest(string AccessPolicyNumber, decimal Balance, Guid TenantId);
public record AccessPolicyResponse(Guid Id, string AccessPolicyNumber, decimal Balance, Guid TenantId);
