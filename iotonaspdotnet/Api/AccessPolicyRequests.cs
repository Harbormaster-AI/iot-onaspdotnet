namespace iotonaspdotnet.Api.AccessPolicy;

public record CreateAccessPolicyRequest(string AccessPolicyNumber, decimal Balance, Guid TenantId);
public record UpdateAccessPolicyRequest(string AccessPolicyNumber, decimal Balance, Guid TenantId);
public record AccessPolicyResponse(Guid Id, string AccessPolicyNumber, decimal Balance, Guid TenantId);
