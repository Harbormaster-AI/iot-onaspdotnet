using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IHardwareModuleService
{
    Task<HardwareModule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<HardwareModule>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class HardwareModuleService : IHardwareModuleService
{
    private readonly IHardwareModuleRepository _repository;
    private readonly IDeviceVendorRepository _deviceVendors;

    public HardwareModuleService(
        IDeviceVendorRepository deviceVendors,
        IHardwareModuleRepository repository )
    {
        _repository = repository;
        _deviceVendors = deviceVendors;
    }

    public Task<HardwareModule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<HardwareModule>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken)
    {
        var deviceVendor.Vendor = await _deviceVendors.GetByIdAsync(hardwareModule.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceVendor not found.");

        if (deviceVendor.Vendor is not null)
        {
            throw new InvalidOperationException("DeviceVendor already has a(n) hardwareModule (1:1 relationship).");
        }
        await _repository.AddAsync(hardwareModule, cancellationToken);
    }

    public async Task<bool> UpdateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(hardwareModule.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a deviceVendor who already has another hardwareModule.
        if (existing.Id != hardwareModule.Id)
        {
            var Vendor = await _deviceVendors.GetByIdAsync(hardwareModule.Id, cancellationToken)
                ?? throw new InvalidOperationException("DeviceVendor not found.");

            if (Vendor.HardwareModule is not null && Vendor.HardwareModule.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceVendor already has an hardwareModule (1:1 relationship).");
            }
        }

        existing.ModuleCode = hardwareModule.ModuleCode;
        existing.DatasheetUri = hardwareModule.DatasheetUri;
        existing.ModuleType = hardwareModule.ModuleType;

        existing.Id = hardwareModule.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
