namespace iotonaspdotnet.Api;


public record CreateMessagingEndpointRequest( , String, Integer, Boolean, MessagingProtocol , Guid TenantId );
public record UpdateMessagingEndpointRequest( , String, Integer, Boolean, MessagingProtocol , Guid TenantId );
public record MessagingEndpointResponse( Guid Id, , String, Integer, Boolean, MessagingProtocol , Guid TenantId );
