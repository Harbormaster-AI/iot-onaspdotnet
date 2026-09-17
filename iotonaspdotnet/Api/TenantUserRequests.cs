namespace iotonaspdotnet.Api;

, String, String, String, UserRole
, Guid TenantId
public record CreateTenantUserRequest(string TenantUserNumber, decimal Balance, Guid TenantId);
public record UpdateTenantUserRequest(string TenantUserNumber, decimal Balance, Guid TenantId);
public record TenantUserResponse(Guid Id, string TenantUserNumber, decimal Balance, Guid TenantId);
