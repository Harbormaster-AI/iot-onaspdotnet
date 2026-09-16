namespace iotonaspdotnet.Api.Tenant;

public record CreateTenantRequest(string TenantNumber, decimal Balance, );
public record UpdateTenantRequest(string TenantNumber, decimal Balance, );
public record TenantResponse(Guid Id, string TenantNumber, decimal Balance, );
