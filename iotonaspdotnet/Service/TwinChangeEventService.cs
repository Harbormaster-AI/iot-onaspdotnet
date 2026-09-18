using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ITwinChangeEventService {

    Task Create(TwinChangeEvent model , CancellationToken cancellationToken);
    Task<bool> Update(TwinChangeEvent model, CancellationToken cancellationToken);
    Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTwin(AssociationRequest request, CancellationToken cancellationToken);


}

public class TwinChangeEventService : ITwinChangeEventService
{
    private readonly ITwinChangeEventRepository _repository;

    public TwinChangeEventService(
        ITwinChangeEventRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(TwinChangeEvent model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(TwinChangeEvent model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.EventId = model.EventId;
        existing.OccurredAt = model.OccurredAt;
        existing.ChangeType = model.ChangeType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
