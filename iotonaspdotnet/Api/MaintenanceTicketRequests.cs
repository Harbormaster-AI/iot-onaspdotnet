namespace iotonaspdotnet.Api;


public record CreateMaintenanceTicketRequest( , String, DateTime, DateTime, MaintenancePriority, MaintenanceStatus , Guid IoTDeviceId, Guid TenantId );
public record UpdateMaintenanceTicketRequest( , String, DateTime, DateTime, MaintenancePriority, MaintenanceStatus , Guid IoTDeviceId, Guid TenantId );
public record MaintenanceTicketResponse( Guid Id, , String, DateTime, DateTime, MaintenancePriority, MaintenanceStatus , Guid IoTDeviceId, Guid TenantId );
