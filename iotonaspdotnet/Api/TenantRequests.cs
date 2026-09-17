namespace iotonaspdotnet.Api;


public record CreateTenantRequest( , String, TenantType  );
public record UpdateTenantRequest( , String, TenantType  );
public record TenantResponse( Guid Id, , String, TenantType  );
