namespace iotonaspdotnet.Api;

, String, Integer, Integer
, Guid TenantId
public record CreateConnectivityPlanRequest(string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
public record UpdateConnectivityPlanRequest(string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
public record ConnectivityPlanResponse(Guid Id, string ConnectivityPlanNumber, decimal Balance, Guid TenantId);
