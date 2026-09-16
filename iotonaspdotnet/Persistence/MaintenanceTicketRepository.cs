using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class MaintenanceTicketRepository : IMaintenanceTicketRepository
{
    private readonly ApplicationDbContext _db;

    public MaintenanceTicketRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MaintenanceTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MaintenanceTickets
            .Include(x => x.IoTDevice)
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaintenanceTicket>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MaintenanceTickets
            .AsNoTracking()
            .Include(x => x.IoTDevice)
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken)
    {
        _db.MaintenanceTickets.Add(maintenanceTicket);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken)
    {
        _db.MaintenanceTickets.Update(maintenanceTicket);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken)
    {
        _db.MaintenanceTickets.Remove(maintenanceTicket);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
