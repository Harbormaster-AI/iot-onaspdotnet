namespace iotonaspdotnet.Api;

public record CreateMessagingEndpointRequest(string MessagingEndpointNumber, decimal Balance, Guid TenantId);
public record UpdateMessagingEndpointRequest(string MessagingEndpointNumber, decimal Balance, Guid TenantId);
public record MessagingEndpointResponse(Guid Id, string MessagingEndpointNumber, decimal Balance, Guid TenantId);
