using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class AlertRuleRepository : IAlertRuleRepository
{
    private readonly ApplicationDbContext _db;

    public AlertRuleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AlertRules
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AlertRules
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Add(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Update(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Remove(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
