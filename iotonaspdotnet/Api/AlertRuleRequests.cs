namespace iotonaspdotnet.Api;

, String, String, AlertSeverity
, Guid TenantId
public record CreateAlertRuleRequest(string AlertRuleNumber, decimal Balance, Guid TenantId);
public record UpdateAlertRuleRequest(string AlertRuleNumber, decimal Balance, Guid TenantId);
public record AlertRuleResponse(Guid Id, string AlertRuleNumber, decimal Balance, Guid TenantId);
