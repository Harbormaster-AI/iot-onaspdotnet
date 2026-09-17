namespace iotonaspdotnet.Api;


public record CreateSensorInstanceRequest( , String, String, Integer, SensorType , Guid IoTDeviceId );
public record UpdateSensorInstanceRequest( , String, String, Integer, SensorType , Guid IoTDeviceId );
public record SensorInstanceResponse( Guid Id, , String, String, Integer, SensorType , Guid IoTDeviceId );
