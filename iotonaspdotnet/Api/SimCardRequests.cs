namespace iotonaspdotnet.Api;

public record CreateSimCardRequest(string SimCardNumber, decimal Balance, Guid TenantId, Guid ConnectivityPlanId);
public record UpdateSimCardRequest(string SimCardNumber, decimal Balance, Guid TenantId, Guid ConnectivityPlanId);
public record SimCardResponse(Guid Id, string SimCardNumber, decimal Balance, Guid TenantId, Guid ConnectivityPlanId);
