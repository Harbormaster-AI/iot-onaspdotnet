
using iotonaspdotnet.Contracts;
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

    public async Task UpdateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        _db.DeviceGroups.Update(deviceGroup);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        _db.DeviceGroups.Remove(deviceGroup);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id) &&
                EF.Property<Guid?>(
                    ioTDevice,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
