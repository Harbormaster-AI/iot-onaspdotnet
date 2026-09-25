
using iotonaspdotnet.Contracts;
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


    public async Task AddToCommandDefinitionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandDefinitions
            .Where(commandDefinition =>
                request.ChildIds.Contains(commandDefinition.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandDefinition =>
                        EF.Property<Guid?>(
                            commandDefinition,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCommandDefinitionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandDefinitions
            .Where(commandDefinition =>
                request.ChildIds.Contains(commandDefinition.Id) &&
                EF.Property<Guid?>(
                    commandDefinition,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandDefinition =>
                        EF.Property<Guid?>(
                            commandDefinition,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
