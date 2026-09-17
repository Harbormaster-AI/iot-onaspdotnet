namespace iotonaspdotnet.Api;


public record CreateEdgeApplicationRequest( , String, String, String, DeploymentStatus , Guid GatewayId );
public record UpdateEdgeApplicationRequest( , String, String, String, DeploymentStatus , Guid GatewayId );
public record EdgeApplicationResponse( Guid Id, , String, String, String, DeploymentStatus , Guid GatewayId );
