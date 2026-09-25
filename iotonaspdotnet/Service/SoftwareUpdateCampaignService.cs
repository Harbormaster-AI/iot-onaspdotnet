
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISoftwareUpdateCampaignRepository _repository;
    private readonly ILogger<SoftwareUpdateCampaignService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SoftwareUpdateCampaignService(
        ApplicationTelemetry telemetry,
        ISoftwareUpdateCampaignRepository repository,
        ILogger<SoftwareUpdateCampaignService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(SoftwareUpdateCampaign model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "SoftwareUpdateCampaign",
                "CreateSoftwareUpdateCampaign",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "SoftwareUpdateCampaign",
                "UpdateSoftwareUpdateCampaign",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "SoftwareUpdateCampaign",
                "UpdateSoftwareUpdateCampaign",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SoftwareUpdateCampaign found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<FirmwareReleaseService>().Get(childRequest, cancellationToken);
            parent.FirmwareRelease = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignFirmwareRelease(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SoftwareUpdateCampaign found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.FirmwareRelease = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SoftwareUpdateCampaign found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DeviceGroupService>().Get(childRequest, cancellationToken);
            parent.DeviceGroup = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignDeviceGroup(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SoftwareUpdateCampaign found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.DeviceGroup = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SoftwareUpdateCampaign",
                "AddToExecutions",
                () => _repository.AddToExecutionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromExecutions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SoftwareUpdateCampaign",
                "RemoveFromExecutions",
                () => _repository.RemoveFromExecutionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
