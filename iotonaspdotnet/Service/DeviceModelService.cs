using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.DeviceVendors;
using iotonaspdotnet.Persistence.TwinTemplates;

namespace iotonaspdotnet.Service

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
        _twinTemplates = twinTemplates;
        _twinTemplates = twinTemplates;
    }

    public Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DeviceModel deviceModel, CancellationToken cancellationToken)
    {
        var deviceVendor = await _deviceVendors.GetByIdAsync(deviceModel.DeviceVendorId, cancellationToken)
            ?? throw new InvalidOperationException("DeviceVendor not found.");

        if (deviceVendor.DeviceModel is not null)
        {
            throw new InvalidOperationException("DeviceVendor already has a(n) deviceModel (1:1 relationship).");
        }

        var twinTemplate = await _twinTemplates.GetByIdAsync(deviceModel.TwinTemplateId, cancellationToken)
            ?? throw new InvalidOperationException("TwinTemplate not found.");

        if (twinTemplate.DeviceModel is not null)
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
        if (existing.TwinTemplateId != deviceModel.TwinTemplateId)
        {
            var target;
            target = await _deviceVendors.GetByIdAsync(deviceModel.DeviceVendorId, cancellationToken)
                ?? throw new InvalidOperationException("DeviceVendor not found.");

            if (target.DeviceModel is not null && target.DeviceModel.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceVendor already has an deviceModel (1:1 relationship).");
            }

        }
            target = await _twinTemplates.GetByIdAsync(deviceModel.TwinTemplateId, cancellationToken)
                ?? throw new InvalidOperationException("TwinTemplate not found.");

            if (target.DeviceModel is not null && target.DeviceModel.Id != existing.Id)
            {
                throw new InvalidOperationException("Target twinTemplate already has an deviceModel (1:1 relationship).");
            }

        }

        existing.attributeName = deviceModel.attributeName;
        existing.attributeName = deviceModel.attributeName;
        existing.attributeName = deviceModel.attributeName;
        existing.attributeName = deviceModel.attributeName;
        existing.attributeName = deviceModel.attributeName;

        existing.DeviceVendorId = deviceModel.DeviceVendorId;
        existing.TwinTemplateId = deviceModel.TwinTemplateId;
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
