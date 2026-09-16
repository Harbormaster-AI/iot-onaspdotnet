using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.DeviceModels;

namespace iotonaspdotnet.Service;

public interface IFirmwareReleaseService
{
    Task<FirmwareRelease?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FirmwareRelease>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class FirmwareReleaseService : IFirmwareReleaseService
{
    private readonly IFirmwareReleaseRepository _repository;
    private readonly IDeviceModelRepository _deviceModels;

    public FirmwareReleaseService(
        IDeviceModelRepository deviceModels,
        IFirmwareReleaseRepository repository )
    {
        _repository = repository;
        _deviceModels = deviceModels;
    }

    public Task<FirmwareRelease?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<FirmwareRelease>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken)
    {
        var deviceModel = await _deviceModels.GetByIdAsync(firmwareRelease.DeviceModelId, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.FirmwareRelease is not null)
        {
            throw new InvalidOperationException("DeviceModel already has a(n) firmwareRelease (1:1 relationship).");
        }

        await _repository.AddAsync(firmwareRelease, cancellationToken);
    }

    public async Task<bool> UpdateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(firmwareRelease.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a deviceModel who already has another firmwareRelease.
        if (existing.DeviceModelId != firmwareRelease.DeviceModelId)
        {
            var DeviceModel = await _deviceModels.GetByIdAsync(firmwareRelease.DeviceModelId, cancellationToken)
                ?? throw new InvalidOperationException("DeviceModel not found.");

            if (DeviceModel.FirmwareRelease is not null && DeviceModel.FirmwareRelease.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceModel already has an firmwareRelease (1:1 relationship).");
            }
        }

        existing.Version = firmwareRelease.Version;
        existing.ReleaseDate = firmwareRelease.ReleaseDate;
        existing.ReleaseNotes = firmwareRelease.ReleaseNotes;
        existing.Checksum = firmwareRelease.Checksum;

        existing.DeviceModelId = firmwareRelease.DeviceModelId;
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
