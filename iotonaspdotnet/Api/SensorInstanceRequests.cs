namespace iotonaspdotnet.Api;

, String, String, Integer, SensorType
, Guid IoTDeviceId
public record CreateSensorInstanceRequest(string SensorInstanceNumber, decimal Balance, Guid IoTDeviceId);
public record UpdateSensorInstanceRequest(string SensorInstanceNumber, decimal Balance, Guid IoTDeviceId);
public record SensorInstanceResponse(Guid Id, string SensorInstanceNumber, decimal Balance, Guid IoTDeviceId);
