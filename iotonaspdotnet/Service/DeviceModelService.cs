using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceModelService
{
    Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DeviceModelService : IDeviceModelService
{
    private readonly IDeviceModelRepository _repository;
    private readonly IDeviceVendorRepository _deviceVendors;
    private readonly ITwinTemplateRepository _twinTemplates;

    public DeviceModelService(
        IDeviceVendorRepository deviceVendors,
        ITwinTemplateRepository twinTemplates,
        IDeviceModelRepository repository )
    {
        _repository = repository;
        _deviceVendors = deviceVendors;
        _twinTemplates = twinTemplates;
    }

    public Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        var deviceVendor = await _deviceVendors.GetByIdAsync(deviceModel.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceVendor not found.");

        if (deviceVendor.Vendor is not null)
        {
            throw new InvalidOperationException("DeviceVendor already has a(n) deviceModel (1:1 relationship).");
        }
        var twinTemplate = await _twinTemplates.GetByIdAsync(deviceModel.Id, cancellationToken)
            ?? throw new InvalidOperationException("TwinTemplate not found.");

        if (twinTemplate.TwinTemplate is not null)
        {
            throw new InvalidOperationException("TwinTemplate already has a(n) deviceModel (1:1 relationship).");
        }
        await _repository.AddAsync(deviceModel, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(deviceModel.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a twinTemplate who already has another deviceModel.
        if (existing.Id != deviceModel.Id)
        {
            var Vendor = await _deviceVendors.GetByIdAsync(deviceModel.Id, cancellationToken)
                ?? throw new InvalidOperationException("DeviceVendor not found.");

            if (Vendor.DeviceModel is not null && Vendor.DeviceModel.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceVendor already has an deviceModel (1:1 relationship).");
            }
            var TwinTemplate = await _twinTemplates.GetByIdAsync(deviceModel.Id, cancellationToken)
                ?? throw new InvalidOperationException("TwinTemplate not found.");

            if (TwinTemplate.DeviceModel is not null && TwinTemplate.DeviceModel.Id != existing.Id)
            {
                throw new InvalidOperationException("Target twinTemplate already has an deviceModel (1:1 relationship).");
            }
        }

        existing.Name = deviceModel.Name;
        existing.ModelNumber = deviceModel.ModelNumber;
        existing.HardwareRevision = deviceModel.HardwareRevision;
        existing.SupportedConnectivity = deviceModel.SupportedConnectivity;
        existing.DefaultTelemetryEncoding = deviceModel.DefaultTelemetryEncoding;

        existing.Id = deviceModel.Id;
        existing.Id = deviceModel.Id;
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
