using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISoftwareUpdateCampaignService

    Task Create(SoftwareUpdateCampaignRequest request , CancellationToken cancellationToken);
    Task<bool> Update(SoftwareUpdateCampaignRequest request, CancellationToken cancellationToken);
    Task<SoftwareUpdateCampaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SoftwareUpdateCampaignService : ISoftwareUpdateCampaignService
{
    private readonly ISoftwareUpdateCampaignRepository _repository;

    public SoftwareUpdateCampaignService(
        ISoftwareUpdateCampaignRepository repository )
    {
        _repository = repository;
    }

    public Task<SoftwareUpdateCampaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(SoftwareUpdateCampaignRequest request, CancellationToken cancellationToken)
    {
        var firmwareRelease = await _firmwareReleases.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("FirmwareRelease not found.");

        if (firmwareRelease.FirmwareRelease is not null)
        {
            throw new InvalidOperationException("FirmwareRelease:FirmwareRelease already has a(n) SoftwareUpdateCampaign (1:1 relationship).");
        }
        var deviceGroup = await _deviceGroups.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceGroup not found.");

        if (deviceGroup.DeviceGroup is not null)
        {
            throw new InvalidOperationException("DeviceGroup:DeviceGroup already has a(n) SoftwareUpdateCampaign (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(SoftwareUpdateCampaignRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.CampaignCode = request.CampaignCode;
        existing.ScheduledStart = request.ScheduledStart;
        existing.ScheduledEnd = request.ScheduledEnd;
        existing.FirmwareRelease = request.FirmwareRelease;
        existing.DeviceGroup = request.DeviceGroup;
        existing.Executions = request.Executions;
        existing.Status = request.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    Task<bool> AssignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
