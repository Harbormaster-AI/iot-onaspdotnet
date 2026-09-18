using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DeviceModelRepository : IDeviceModelRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceModelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DeviceModels
            .Include(x => x.Vendor)
            .Include(x => x.TwinTemplate)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DeviceModels
            .AsNoTracking()
            .Include(x => x.Vendor)
            .Include(x => x.TwinTemplate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        _db.DeviceModels.Add(deviceModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        _db.DeviceModels.Update(deviceModel);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        _db.DeviceModels.Remove(deviceModel);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
