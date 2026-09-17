namespace iotonaspdotnet.Api;


public record CreateAlertRequest( , DateTime, DateTime, String, AlertStatus , Guid IoTDeviceId, Guid AlertRuleId );
public record UpdateAlertRequest( , DateTime, DateTime, String, AlertStatus , Guid IoTDeviceId, Guid AlertRuleId );
public record AlertResponse( Guid Id, , DateTime, DateTime, String, AlertStatus , Guid IoTDeviceId, Guid AlertRuleId );
