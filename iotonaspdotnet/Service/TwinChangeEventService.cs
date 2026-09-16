using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.DigitalTwins;

namespace iotonaspdotnet.Service

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
        var digitalTwin = await _digitalTwins.GetByIdAsync(twinChangeEvent.DigitalTwinId, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.TwinChangeEvent is not null)
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
        if (existing.DigitalTwinId != twinChangeEvent.DigitalTwinId)
        {
            var target;
            target = await _digitalTwins.GetByIdAsync(twinChangeEvent.DigitalTwinId, cancellationToken)
                ?? throw new InvalidOperationException("DigitalTwin not found.");

            if (target.TwinChangeEvent is not null && target.TwinChangeEvent.Id != existing.Id)
            {
                throw new InvalidOperationException("Target digitalTwin already has an twinChangeEvent (1:1 relationship).");
            }

        }

        existing.attributeName = twinChangeEvent.attributeName;
        existing.attributeName = twinChangeEvent.attributeName;
        existing.attributeName = twinChangeEvent.attributeName;

        existing.DigitalTwinId = twinChangeEvent.DigitalTwinId;
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
