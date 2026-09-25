
using iotonaspdotnet.Contracts;
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

    public async Task UpdateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        _db.DeviceVendors.Update(deviceVendor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        _db.DeviceVendors.Remove(deviceVendor);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDeviceModelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceModels
            .Where(deviceModel =>
                request.ChildIds.Contains(deviceModel.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceModel =>
                        EF.Property<Guid?>(
                            deviceModel,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDeviceModelsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceModels
            .Where(deviceModel =>
                request.ChildIds.Contains(deviceModel.Id) &&
                EF.Property<Guid?>(
                    deviceModel,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceModel =>
                        EF.Property<Guid?>(
                            deviceModel,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToFirmwareReleasesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FirmwareReleases
            .Where(firmwareRelease =>
                request.ChildIds.Contains(firmwareRelease.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    firmwareRelease =>
                        EF.Property<Guid?>(
                            firmwareRelease,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFirmwareReleasesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.FirmwareReleases
            .Where(firmwareRelease =>
                request.ChildIds.Contains(firmwareRelease.Id) &&
                EF.Property<Guid?>(
                    firmwareRelease,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    firmwareRelease =>
                        EF.Property<Guid?>(
                            firmwareRelease,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToHardwareModulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.HardwareModules
            .Where(hardwareModule =>
                request.ChildIds.Contains(hardwareModule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    hardwareModule =>
                        EF.Property<Guid?>(
                            hardwareModule,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromHardwareModulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.HardwareModules
            .Where(hardwareModule =>
                request.ChildIds.Contains(hardwareModule.Id) &&
                EF.Property<Guid?>(
                    hardwareModule,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    hardwareModule =>
                        EF.Property<Guid?>(
                            hardwareModule,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
