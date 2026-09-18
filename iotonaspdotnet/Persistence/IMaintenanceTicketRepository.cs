using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IMaintenanceTicketRepository
{
    Task<MaintenanceTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceTicket>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken);
    Task UpdateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken);
    Task DeleteAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken);
}
