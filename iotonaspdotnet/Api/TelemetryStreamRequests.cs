namespace iotonaspdotnet.Api;


public record CreateTelemetryStreamRequest( , String, Integer, MessageQoS , Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId );
public record UpdateTelemetryStreamRequest( , String, Integer, MessageQoS , Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId );
public record TelemetryStreamResponse( Guid Id, , String, Integer, MessageQoS , Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId );
