namespace iotonaspdotnet.Api;

public record CreateDataRetentionPolicyRequest(string DataRetentionPolicyNumber, decimal Balance, Guid TenantId);
public record UpdateDataRetentionPolicyRequest(string DataRetentionPolicyNumber, decimal Balance, Guid TenantId);
public record DataRetentionPolicyResponse(Guid Id, string DataRetentionPolicyNumber, decimal Balance, Guid TenantId);
