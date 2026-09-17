namespace iotonaspdotnet.Api;

public record CreateAlertRequest(string AlertNumber, decimal Balance, Guid IoTDeviceId, Guid AlertRuleId);
public record UpdateAlertRequest(string AlertNumber, decimal Balance, Guid IoTDeviceId, Guid AlertRuleId);
public record AlertResponse(Guid Id, string AlertNumber, decimal Balance, Guid IoTDeviceId, Guid AlertRuleId);
