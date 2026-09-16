namespace iotonaspdotnet.Api.SimCard;

public record CreateSimCardRequest(string SimCardNumber, decimal Balance , , Guid TenantIdGuid ConnectivityPlanId);
public record UpdateSimCardRequest(string SimCardNumber, decimal Balance , , Guid TenantIdGuid ConnectivityPlanId);
public record SimCardResponse(Guid Id, string SimCardNumber, decimal Balance , , Guid TenantIdGuid ConnectivityPlanId);
