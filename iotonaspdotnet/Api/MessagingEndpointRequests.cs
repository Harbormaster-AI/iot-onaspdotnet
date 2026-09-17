namespace iotonaspdotnet.Api;

, String, Integer, Boolean, MessagingProtocol
, Guid TenantId
public record CreateMessagingEndpointRequest(string MessagingEndpointNumber, decimal Balance, Guid TenantId);
public record UpdateMessagingEndpointRequest(string MessagingEndpointNumber, decimal Balance, Guid TenantId);
public record MessagingEndpointResponse(Guid Id, string MessagingEndpointNumber, decimal Balance, Guid TenantId);
