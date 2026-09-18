using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class ConnectivityPlanRepository : IConnectivityPlanRepository
{
    private readonly ApplicationDbContext _db;

    public ConnectivityPlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ConnectivityPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ConnectivityPlans
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ConnectivityPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ConnectivityPlans
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken)
    {
        _db.ConnectivityPlans.Add(connectivityPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken)
    {
        _db.ConnectivityPlans.Update(connectivityPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken)
    {
        _db.ConnectivityPlans.Remove(connectivityPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
