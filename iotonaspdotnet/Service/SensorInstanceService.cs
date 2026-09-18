using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ISensorInstanceService {

    Task Create(SensorInstance model , CancellationToken cancellationToken);
    Task<bool> Update(SensorInstance model, CancellationToken cancellationToken);
    Task<SensorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SensorInstance>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SensorInstanceService : ISensorInstanceService
{
    private readonly ISensorInstanceRepository _repository;

    public SensorInstanceService(
        ISensorInstanceRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(SensorInstance model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(SensorInstance model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.Unit = model.Unit;
        existing.SamplingIntervalMs = model.SamplingIntervalMs;
        existing.SensorType = model.SensorType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<SensorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SensorInstance>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
