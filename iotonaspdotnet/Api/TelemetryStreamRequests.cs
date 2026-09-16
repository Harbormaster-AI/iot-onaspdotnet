namespace iotonaspdotnet.Api.TelemetryStream;

public record CreateTelemetryStreamRequest(string TelemetryStreamNumber, decimal Balance , , , , , Guid IoTDeviceIdGuid SensorInstanceIdGuid TelemetrySchemaIdGuid MessagingEndpointIdGuid DataRetentionPolicyId);
public record UpdateTelemetryStreamRequest(string TelemetryStreamNumber, decimal Balance , , , , , Guid IoTDeviceIdGuid SensorInstanceIdGuid TelemetrySchemaIdGuid MessagingEndpointIdGuid DataRetentionPolicyId);
public record TelemetryStreamResponse(Guid Id, string TelemetryStreamNumber, decimal Balance , , , , , Guid IoTDeviceIdGuid SensorInstanceIdGuid TelemetrySchemaIdGuid MessagingEndpointIdGuid DataRetentionPolicyId);
