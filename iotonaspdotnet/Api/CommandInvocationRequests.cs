namespace iotonaspdotnet.Api;

, String, DateTime, DateTime, CommandStatus
, Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId
public record CreateCommandInvocationRequest(string CommandInvocationNumber, decimal Balance, Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId);
public record UpdateCommandInvocationRequest(string CommandInvocationNumber, decimal Balance, Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId);
public record CommandInvocationResponse(Guid Id, string CommandInvocationNumber, decimal Balance, Guid IoTDeviceId, Guid CommandDefinitionId, Guid ActuatorInstanceId, Guid TenantUserId);
