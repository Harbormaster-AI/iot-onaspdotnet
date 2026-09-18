using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class AccessPolicyRepository : IAccessPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public AccessPolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AccessPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AccessPolicys
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AccessPolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AccessPolicys
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Add(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Update(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Remove(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
