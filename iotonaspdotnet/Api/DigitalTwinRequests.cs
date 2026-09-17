namespace iotonaspdotnet.Api;


public record CreateDigitalTwinRequest( , String, Integer, Integer, DateTime , Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId );
public record UpdateDigitalTwinRequest( , String, Integer, Integer, DateTime , Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId );
public record DigitalTwinResponse( Guid Id, , String, Integer, Integer, DateTime , Guid IoTDeviceId, Guid GatewayId, Guid TwinTemplateId );
