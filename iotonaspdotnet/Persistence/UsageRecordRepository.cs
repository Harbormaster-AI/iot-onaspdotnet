using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class UsageRecordRepository : IUsageRecordRepository
{
    private readonly ApplicationDbContext _db;

    public UsageRecordRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UsageRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.UsageRecords
            .Include(x => x.Tenant)
            .Include(x => x.IoTDevice)
            .Include(x => x.ConnectivityPlan)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<UsageRecord>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.UsageRecords
            .AsNoTracking()
            .Include(x => x.Tenant)
            .Include(x => x.IoTDevice)
            .Include(x => x.ConnectivityPlan)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UsageRecord usageRecord, CancellationToken cancellationToken)
    {
        _db.UsageRecords.Add(usageRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UsageRecord usageRecord, CancellationToken cancellationToken)
    {
        _db.UsageRecords.Update(usageRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UsageRecord usageRecord, CancellationToken cancellationToken)
    {
        _db.UsageRecords.Remove(usageRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
