
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class AlertRepository : IAlertRepository
{
    private readonly ApplicationDbContext _db;

    public AlertRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Alerts
            .Include(x => x.Device)
            .Include(x => x.AlertRule)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Alert>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Alerts
            .AsNoTracking()
            .Include(x => x.Device)
            .Include(x => x.AlertRule)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Alert alert, CancellationToken cancellationToken)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Alert alert, CancellationToken cancellationToken)
    {
        _db.Alerts.Update(alert);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Alert alert, CancellationToken cancellationToken)
    {
        _db.Alerts.Remove(alert);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
