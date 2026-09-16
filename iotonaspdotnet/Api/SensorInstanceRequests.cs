namespace iotonaspdotnet.Api.SensorInstance;

public record CreateSensorInstanceRequest(string SensorInstanceNumber, decimal Balance , Guid IoTDeviceId);
public record UpdateSensorInstanceRequest(string SensorInstanceNumber, decimal Balance , Guid IoTDeviceId);
public record SensorInstanceResponse(Guid Id, string SensorInstanceNumber, decimal Balance , Guid IoTDeviceId);
