namespace iotonaspdotnet.Api;


public record CreateTenantUserRequest( , String, String, String, UserRole , Guid TenantId );
public record UpdateTenantUserRequest( , String, String, String, UserRole , Guid TenantId );
public record TenantUserResponse( Guid Id, , String, String, String, UserRole , Guid TenantId );
