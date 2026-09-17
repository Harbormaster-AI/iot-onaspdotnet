namespace iotonaspdotnet.Api;


public record CreateCommandInvocationRequest( , String, DateTime, DateTime, CommandStatus , Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId );
public record UpdateCommandInvocationRequest( , String, DateTime, DateTime, CommandStatus , Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId );
public record CommandInvocationResponse( Guid Id, , String, DateTime, DateTime, CommandStatus , Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId );
