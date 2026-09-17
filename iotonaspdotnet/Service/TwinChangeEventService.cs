using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITwinChangeEventService
{
    Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken);
    Task Create(TwinChangeEventRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TwinChangeEventRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class TwinChangeEventService : ITwinChangeEventService
{
    private readonly ITwinChangeEventRepository _repository;

    public TwinChangeEventService(
        ITwinChangeEventRepository repository )
    {
        _repository = repository;
    }

    public Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TwinChangeEventRequest request, CancellationToken cancellationToken)
    {
        var digitalTwin = await _digitalTwins.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.Twin is not null)
        {
            throw new InvalidOperationException("DigitalTwin:Twin already has a(n) TwinChangeEvent (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TwinChangeEventRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.EventId = request.EventId
        existing.OccurredAt = request.OccurredAt
        existing.Twin = request.Twin
        existing.ChangeType = request.ChangeType
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.EventId = twinChangeEvent.EventId;
        existing.OccurredAt = twinChangeEvent.OccurredAt;
        existing.ChangeType = twinChangeEvent.ChangeType;

        existing.Id = twinChangeEvent.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
