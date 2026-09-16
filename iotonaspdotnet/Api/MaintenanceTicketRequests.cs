namespace iotonaspdotnet.Api.MaintenanceTicket;

public record CreateMaintenanceTicketRequest(string MaintenanceTicketNumber, decimal Balance , , Guid IoTDeviceIdGuid TenantId);
public record UpdateMaintenanceTicketRequest(string MaintenanceTicketNumber, decimal Balance , , Guid IoTDeviceIdGuid TenantId);
public record MaintenanceTicketResponse(Guid Id, string MaintenanceTicketNumber, decimal Balance , , Guid IoTDeviceIdGuid TenantId);
