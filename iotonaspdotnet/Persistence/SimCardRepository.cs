using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SimCardRepository : ISimCardRepository
{
    private readonly ApplicationDbContext _db;

    public SimCardRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SimCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SimCards
            .Include(x => x.Tenant)
            .Include(x => x.ConnectivityPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SimCard>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SimCards
            .AsNoTracking()
            .Include(x => x.Tenant)
            .Include(x => x.ConnectivityPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SimCard simCard, CancellationToken cancellationToken)
    {
        _db.SimCards.Add(simCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (SimCard simCard, CancellationToken cancellationToken)
    {
        _db.SimCards.Update(simCard);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SimCard simCard, CancellationToken cancellationToken)
    {
        _db.SimCards.Remove(simCard);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
