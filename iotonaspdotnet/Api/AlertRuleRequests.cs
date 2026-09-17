namespace iotonaspdotnet.Api;


public record CreateAlertRuleRequest( , String, String, AlertSeverity , Guid TenantId );
public record UpdateAlertRuleRequest( , String, String, AlertSeverity , Guid TenantId );
public record AlertRuleResponse( Guid Id, , String, String, AlertSeverity , Guid TenantId );
