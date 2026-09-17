namespace iotonaspdotnet.Api;


public record CreateConnectivityPlanRequest( , String, Integer, Integer , Guid TenantId );
public record UpdateConnectivityPlanRequest( , String, Integer, Integer , Guid TenantId );
public record ConnectivityPlanResponse( Guid Id, , String, Integer, Integer , Guid TenantId );
