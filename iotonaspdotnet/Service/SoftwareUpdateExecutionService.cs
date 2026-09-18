using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ISoftwareUpdateExecutionService {

    Task Create(SoftwareUpdateExecutionRequest request , CancellationToken cancellationToken);
    Task<bool> Update(SoftwareUpdateExecutionRequest request, CancellationToken cancellationToken);
    Task<SoftwareUpdateExecution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateExecution>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);


}

public class SoftwareUpdateExecutionService : ISoftwareUpdateExecutionService
{
    private readonly ISoftwareUpdateExecutionRepository _repository;

    public SoftwareUpdateExecutionService(
        ISoftwareUpdateExecutionRepository repository )
    {
        _repository = repository;
    }

    public Task<SoftwareUpdateExecution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateExecution>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(SoftwareUpdateExecutionRequest request, CancellationToken cancellationToken)
    {
        var softwareUpdateCampaign = await _softwareUpdateCampaigns.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("SoftwareUpdateCampaign not found.");

        if (softwareUpdateCampaign.Campaign is not null)
        {
            throw new InvalidOperationException("SoftwareUpdateCampaign:Campaign already has a(n) SoftwareUpdateExecution (1:1 relationship).");
        }
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) SoftwareUpdateExecution (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(SoftwareUpdateExecutionRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.StartedAt = request.StartedAt;
        existing.CompletedAt = request.CompletedAt;
        existing.Campaign = request.Campaign;
        existing.Device = request.Device;
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

    Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
