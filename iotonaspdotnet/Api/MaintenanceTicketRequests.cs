namespace iotonaspdotnet.Api;

, String, DateTime, DateTime, MaintenancePriority, MaintenanceStatus
, Guid IoTDeviceId, Guid TenantId
public record CreateMaintenanceTicketRequest(string MaintenanceTicketNumber, decimal Balance, Guid IoTDeviceId, Guid TenantId);
public record UpdateMaintenanceTicketRequest(string MaintenanceTicketNumber, decimal Balance, Guid IoTDeviceId, Guid TenantId);
public record MaintenanceTicketResponse(Guid Id, string MaintenanceTicketNumber, decimal Balance, Guid IoTDeviceId, Guid TenantId);
