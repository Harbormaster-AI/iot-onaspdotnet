using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ITelemetryStreamService {

    Task Create(TelemetryStream model , CancellationToken cancellationToken);
    Task<bool> Update(TelemetryStream model, CancellationToken cancellationToken);
    Task<TelemetryStream?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetryStream>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSensor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSensor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSchema(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSchema(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignMessagingEndpoint(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignMessagingEndpoint(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRetentionPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRetentionPolicy(AssociationRequest request, CancellationToken cancellationToken);


}

public class TelemetryStreamService : ITelemetryStreamService
{
    private readonly ITelemetryStreamRepository _repository;

    public TelemetryStreamService(
        ITelemetryStreamRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(TelemetryStream model, CancellationToken cancellationToken)
    {

 
 
 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(TelemetryStream model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.StreamName = model.StreamName;
        existing.RetentionDays = model.RetentionDays;
        existing.Qos = model.Qos;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<TelemetryStream?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TelemetryStream>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSensor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSensor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSchema(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSchema(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignMessagingEndpoint(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignMessagingEndpoint(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRetentionPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRetentionPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
