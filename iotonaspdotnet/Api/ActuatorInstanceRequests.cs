namespace iotonaspdotnet.Api;


public record CreateActuatorInstanceRequest( , String, TopicName, ActuatorType , Guid IoTDeviceId );
public record UpdateActuatorInstanceRequest( , String, TopicName, ActuatorType , Guid IoTDeviceId );
public record ActuatorInstanceResponse( Guid Id, , String, TopicName, ActuatorType , Guid IoTDeviceId );
