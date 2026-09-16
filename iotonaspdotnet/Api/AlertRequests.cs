namespace iotonaspdotnet.Api.Alert;

public record CreateAlertRequest(string AlertNumber, decimal Balance , , Guid IoTDeviceIdGuid AlertRuleId);
public record UpdateAlertRequest(string AlertNumber, decimal Balance , , Guid IoTDeviceIdGuid AlertRuleId);
public record AlertResponse(Guid Id, string AlertNumber, decimal Balance , , Guid IoTDeviceIdGuid AlertRuleId);
