using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class IoTDeviceRepository : IIoTDeviceRepository
{
    private readonly ApplicationDbContext _db;

    public IoTDeviceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.IoTDevices
            .Include(x => x.DeviceModel)
            .Include(x => x.Tenant)
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.Gateway)
            .Include(x => x.DigitalTwin)
            .Include(x => x.ProvisioningRecord)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.IoTDevices
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Add(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Update(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Remove(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
