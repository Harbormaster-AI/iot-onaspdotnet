using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ISoftwareUpdateCampaignService {

    Task Create(SoftwareUpdateCampaign model , CancellationToken cancellationToken);
    Task<bool> Update(SoftwareUpdateCampaign model, CancellationToken cancellationToken);
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
    private readonly ILogger<SoftwareUpdateCampaignService> _logger;

    public SoftwareUpdateCampaignService(
        ISoftwareUpdateCampaignRepository repository, ILogger<SoftwareUpdateCampaignService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SoftwareUpdateCampaign model, CancellationToken cancellationToken)
    {

 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(SoftwareUpdateCampaign model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CampaignCode = model.CampaignCode;
            existing.ScheduledStart = model.ScheduledStart;
            existing.ScheduledEnd = model.ScheduledEnd;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<SoftwareUpdateCampaign?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
