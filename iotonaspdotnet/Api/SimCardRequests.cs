namespace iotonaspdotnet.Api;


public record CreateSimCardRequest( , String, String, String, SimStatus , Guid TenantId, Guid ConnectivityPlanId );
public record UpdateSimCardRequest( , String, String, String, SimStatus , Guid TenantId, Guid ConnectivityPlanId );
public record SimCardResponse( Guid Id, , String, String, String, SimStatus , Guid TenantId, Guid ConnectivityPlanId );
