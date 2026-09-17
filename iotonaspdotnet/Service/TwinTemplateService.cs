using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITwinTemplateService
{
    Task<TwinTemplate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinTemplate>> GetAll(CancellationToken cancellationToken);
    Task Create(TwinTemplateRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TwinTemplateRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class TwinTemplateService : ITwinTemplateService
{
    private readonly ITwinTemplateRepository _repository;

    public TwinTemplateService(
        ITwinTemplateRepository repository )
    {
        _repository = repository;
    }

    public Task<TwinTemplate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<TwinTemplate>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TwinTemplateRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TwinTemplateRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name
        existing.SchemaUri = request.SchemaUri
        existing.Version = request.Version
        existing.DeviceModels = request.DeviceModels
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = twinTemplate.Name;
        existing.SchemaUri = twinTemplate.SchemaUri;
        existing.Version = twinTemplate.Version;

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
