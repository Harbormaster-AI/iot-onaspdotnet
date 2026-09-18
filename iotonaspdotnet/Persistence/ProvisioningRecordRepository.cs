using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class ProvisioningRecordRepository : IProvisioningRecordRepository
{
    private readonly ApplicationDbContext _db;

    public ProvisioningRecordRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProvisioningRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ProvisioningRecords
            .Include(x => x.Device)
            .Include(x => x.Certificate)
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProvisioningRecord>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ProvisioningRecords
            .AsNoTracking()
            .Include(x => x.IoTDevice)
            .Include(x => x.DeviceCertificate)
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken)
    {
        _db.ProvisioningRecords.Add(provisioningRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken)
    {
        _db.ProvisioningRecords.Update(provisioningRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken)
    {
        _db.ProvisioningRecords.Remove(provisioningRecord);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
