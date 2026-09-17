using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITwinChangeEventService
{
    Task<TwinChangeEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinChangeEvent>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TwinChangeEventService : ITwinChangeEventService
{
    private readonly ITwinChangeEventRepository _repository;
    private readonly IDigitalTwinRepository _digitalTwins;

    public TwinChangeEventService(
        IDigitalTwinRepository digitalTwins,
        ITwinChangeEventRepository repository )
    {
        _repository = repository;
        _digitalTwins = digitalTwins;
    }

    public Task<TwinChangeEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TwinChangeEvent>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken)
    {
        var digitalTwin = await _digitalTwins.GetByIdAsync(twinChangeEvent.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.Twin is not null)
        {
            throw new InvalidOperationException("DigitalTwin already has a(n) twinChangeEvent (1:1 relationship).");
        }
        await _repository.AddAsync(twinChangeEvent, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(twinChangeEvent.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a digitalTwin who already has another twinChangeEvent.
        if (existing.Id != twinChangeEvent.Id)
        {
            var Twin = await _digitalTwins.GetByIdAsync(twinChangeEvent.Id, cancellationToken)
                ?? throw new InvalidOperationException("DigitalTwin not found.");

            if (Twin.TwinChangeEvent is not null && Twin.TwinChangeEvent.Id != existing.Id)
            {
                throw new InvalidOperationException("Target digitalTwin already has an twinChangeEvent (1:1 relationship).");
            }
        }

        existing.EventId = twinChangeEvent.EventId;
        existing.OccurredAt = twinChangeEvent.OccurredAt;
        existing.ChangeType = twinChangeEvent.ChangeType;

        existing.Id = twinChangeEvent.Id;
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
