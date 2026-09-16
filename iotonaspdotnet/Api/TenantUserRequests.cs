namespace iotonaspdotnet.Api.TenantUser;

public record CreateTenantUserRequest(string TenantUserNumber, decimal Balance, Guid TenantId);
public record UpdateTenantUserRequest(string TenantUserNumber, decimal Balance, Guid TenantId);
public record TenantUserResponse(Guid Id, string TenantUserNumber, decimal Balance, Guid TenantId);
