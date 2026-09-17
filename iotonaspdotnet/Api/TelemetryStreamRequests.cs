namespace iotonaspdotnet.Api.TelemetryStream;

public record CreateTelemetryStreamRequest(string TelemetryStreamNumber, decimal Balance, Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId);
public record UpdateTelemetryStreamRequest(string TelemetryStreamNumber, decimal Balance, Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId);
public record TelemetryStreamResponse(Guid Id, string TelemetryStreamNumber, decimal Balance, Guid IoTDeviceId, Guid SensorInstanceId, Guid TelemetrySchemaId, Guid MessagingEndpointId, Guid DataRetentionPolicyId);
