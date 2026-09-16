using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service

public interface ITwinTemplateService
{
    Task<TwinTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinTemplate>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TwinTemplateService : ITwinTemplateService
{
    private readonly ITwinTemplateRepository _repository;

    public TwinTemplateService(
        ITwinTemplateRepository repository )
    {
        _repository = repository;
    }

    public Task<TwinTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TwinTemplate>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(twinTemplate, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(twinTemplate.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a twinTemplate who already has another twinTemplate.
        if (existing.TwinTemplateId != twinTemplate.TwinTemplateId)
        {
            var target;
        }

        existing.Name = twinTemplate.Name;
        existing.SchemaUri = twinTemplate.SchemaUri;
        existing.Version = twinTemplate.Version;

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
