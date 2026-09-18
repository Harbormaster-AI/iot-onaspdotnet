using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DeviceGroupRepository : IDeviceGroupRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceGroupRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DeviceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DeviceGroups
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DeviceGroup>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DeviceGroups
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        _db.DeviceGroups.Add(deviceGroup);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        _db.DeviceGroups.Update(deviceGroup);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        _db.DeviceGroups.Remove(deviceGroup);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
