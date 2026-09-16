using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.FirmwareReleases;
using iotonaspdotnet.Persistence.DeviceGroups;

namespace iotonaspdotnet.Service

public interface ISoftwareUpdateCampaignService
{
    Task<SoftwareUpdateCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class SoftwareUpdateCampaignService : ISoftwareUpdateCampaignService
{
    private readonly ISoftwareUpdateCampaignRepository _repository;
    private readonly IFirmwareReleaseRepository _firmwareReleases;
    private readonly IDeviceGroupRepository _deviceGroups;

    public SoftwareUpdateCampaignService(
        IFirmwareReleaseRepository firmwareReleases,
        IDeviceGroupRepository deviceGroups,
        ISoftwareUpdateCampaignRepository repository )
    {
        _repository = repository;
        _deviceGroups = deviceGroups;
        _deviceGroups = deviceGroups;
    }

    public Task<SoftwareUpdateCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken)
    {
        var firmwareRelease = await _firmwareReleases.GetByIdAsync(softwareUpdateCampaign.FirmwareReleaseId, cancellationToken)
            ?? throw new InvalidOperationException("FirmwareRelease not found.");

        if (firmwareRelease.SoftwareUpdateCampaign is not null)
        {
            throw new InvalidOperationException("FirmwareRelease already has a(n) softwareUpdateCampaign (1:1 relationship).");
        }

        var deviceGroup = await _deviceGroups.GetByIdAsync(softwareUpdateCampaign.DeviceGroupId, cancellationToken)
            ?? throw new InvalidOperationException("DeviceGroup not found.");

        if (deviceGroup.SoftwareUpdateCampaign is not null)
        {
            throw new InvalidOperationException("DeviceGroup already has a(n) softwareUpdateCampaign (1:1 relationship).");
        }

        await _repository.AddAsync(softwareUpdateCampaign, cancellationToken);
    }

    public async Task<bool> UpdateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(softwareUpdateCampaign.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a deviceGroup who already has another softwareUpdateCampaign.
        if (existing.DeviceGroupId != softwareUpdateCampaign.DeviceGroupId)
        {
            var target;
            target = await _firmwareReleases.GetByIdAsync(softwareUpdateCampaign.FirmwareReleaseId, cancellationToken)
                ?? throw new InvalidOperationException("FirmwareRelease not found.");

            if (target.SoftwareUpdateCampaign is not null && target.SoftwareUpdateCampaign.Id != existing.Id)
            {
                throw new InvalidOperationException("Target firmwareRelease already has an softwareUpdateCampaign (1:1 relationship).");
            }
            target = await _deviceGroups.GetByIdAsync(softwareUpdateCampaign.DeviceGroupId, cancellationToken)
                ?? throw new InvalidOperationException("DeviceGroup not found.");

            if (target.SoftwareUpdateCampaign is not null && target.SoftwareUpdateCampaign.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceGroup already has an softwareUpdateCampaign (1:1 relationship).");
            }
        }

        existing.CampaignCode = softwareUpdateCampaign.CampaignCode;
        existing.ScheduledStart = softwareUpdateCampaign.ScheduledStart;
        existing.ScheduledEnd = softwareUpdateCampaign.ScheduledEnd;
        existing.Status = softwareUpdateCampaign.Status;

        existing.FirmwareReleaseId = softwareUpdateCampaign.FirmwareReleaseId;
        existing.DeviceGroupId = softwareUpdateCampaign.DeviceGroupId;
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
