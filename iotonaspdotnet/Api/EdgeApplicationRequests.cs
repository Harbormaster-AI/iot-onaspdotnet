namespace iotonaspdotnet.Api;

, String, String, String, DeploymentStatus
, Guid GatewayId
public record CreateEdgeApplicationRequest(string EdgeApplicationNumber, decimal Balance, Guid GatewayId);
public record UpdateEdgeApplicationRequest(string EdgeApplicationNumber, decimal Balance, Guid GatewayId);
public record EdgeApplicationResponse(Guid Id, string EdgeApplicationNumber, decimal Balance, Guid GatewayId);
