namespace iotonaspdotnet.Api.DigitalTwin;

public record CreateDigitalTwinRequest(string DigitalTwinNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid TwinTemplateId);
public record UpdateDigitalTwinRequest(string DigitalTwinNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid TwinTemplateId);
public record DigitalTwinResponse(Guid Id, string DigitalTwinNumber, decimal Balance , , , Guid IoTDeviceIdGuid GatewayIdGuid TwinTemplateId);
