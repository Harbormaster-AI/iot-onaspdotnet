using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DeviceVendorRepository : IDeviceVendorRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceVendorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DeviceVendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DeviceVendors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DeviceVendor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DeviceVendors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        _db.DeviceVendors.Add(deviceVendor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        _db.DeviceVendors.Update(deviceVendor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        _db.DeviceVendors.Remove(deviceVendor);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
