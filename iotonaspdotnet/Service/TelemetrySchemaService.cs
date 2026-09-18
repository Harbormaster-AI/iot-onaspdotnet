using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITelemetrySchemaService

    Task Create(TelemetrySchemaRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TelemetrySchemaRequest request, CancellationToken cancellationToken);
    Task<TelemetrySchema?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetrySchema>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TelemetrySchemaService : ITelemetrySchemaService
{
    private readonly ITelemetrySchemaRepository _repository;

    public TelemetrySchemaService(
        ITelemetrySchemaRepository repository )
    {
        _repository = repository;
    }

    public Task<TelemetrySchema?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<TelemetrySchema>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TelemetrySchemaRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TelemetrySchemaRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.SchemaId = request.SchemaId;
        existing.SchemaUri = request.SchemaUri;
        existing.Streams = request.Streams;
        existing.Encoding = request.Encoding;

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


    Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
