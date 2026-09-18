using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DataRetentionPolicyRepository : IDataRetentionPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public DataRetentionPolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataRetentionPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataRetentionPolicys
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataRetentionPolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataRetentionPolicys
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken)
    {
        _db.DataRetentionPolicys.Add(dataRetentionPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken)
    {
        _db.DataRetentionPolicys.Update(dataRetentionPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken)
    {
        _db.DataRetentionPolicys.Remove(dataRetentionPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
