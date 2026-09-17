namespace iotonaspdotnet.Api;

, String, Integer, Integer, DateTime
, Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId
public record CreateDigitalTwinRequest(string DigitalTwinNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId);
public record UpdateDigitalTwinRequest(string DigitalTwinNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId);
public record DigitalTwinResponse(Guid Id, string DigitalTwinNumber, decimal Balance, Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId);
