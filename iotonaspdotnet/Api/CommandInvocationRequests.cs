namespace iotonaspdotnet.Api.CommandInvocation;

public record CreateCommandInvocationRequest(string CommandInvocationNumber, decimal Balance , , , , Guid IoTDeviceIdGuid CommandDefinitionIdGuid ActuatorInstanceIdGuid TenantUserId);
public record UpdateCommandInvocationRequest(string CommandInvocationNumber, decimal Balance , , , , Guid IoTDeviceIdGuid CommandDefinitionIdGuid ActuatorInstanceIdGuid TenantUserId);
public record CommandInvocationResponse(Guid Id, string CommandInvocationNumber, decimal Balance , , , , Guid IoTDeviceIdGuid CommandDefinitionIdGuid ActuatorInstanceIdGuid TenantUserId);
