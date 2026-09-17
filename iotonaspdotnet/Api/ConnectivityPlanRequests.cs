namespace iotonaspdotnet.Api;

public record CreateConnectivityPlanRequest(string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
public record UpdateConnectivityPlanRequest(string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
public record ConnectivityPlanResponse(Guid Id, string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
