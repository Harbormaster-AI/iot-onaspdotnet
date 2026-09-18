using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ISoftwareUpdateExecutionService {

    Task Create(SoftwareUpdateExecution model , CancellationToken cancellationToken);
    Task<bool> Update(SoftwareUpdateExecution model, CancellationToken cancellationToken);
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


    public async Task Create(SoftwareUpdateExecution model, CancellationToken cancellationToken)
    {

 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(SoftwareUpdateExecution model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.StartedAt = model.StartedAt;
        existing.CompletedAt = model.CompletedAt;
        existing.Status = model.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<SoftwareUpdateExecution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateExecution>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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

    public async Task<bool> AssignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCampaign(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
