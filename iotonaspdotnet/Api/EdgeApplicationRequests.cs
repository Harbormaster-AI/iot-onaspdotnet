namespace iotonaspdotnet.Api.EdgeApplication;

public record CreateEdgeApplicationRequest(string EdgeApplicationNumber, decimal Balance , Guid GatewayId);
public record UpdateEdgeApplicationRequest(string EdgeApplicationNumber, decimal Balance , Guid GatewayId);
public record EdgeApplicationResponse(Guid Id, string EdgeApplicationNumber, decimal Balance , Guid GatewayId);
