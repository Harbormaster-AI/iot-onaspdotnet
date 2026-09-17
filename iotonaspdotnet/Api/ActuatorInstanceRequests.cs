namespace iotonaspdotnet.Api.ActuatorInstance;

public record CreateActuatorInstanceRequest(string ActuatorInstanceNumber, decimal Balance, Guid IoTDeviceId);
public record UpdateActuatorInstanceRequest(string ActuatorInstanceNumber, decimal Balance, Guid IoTDeviceId);
public record ActuatorInstanceResponse(Guid Id, string ActuatorInstanceNumber, decimal Balance, Guid IoTDeviceId);
