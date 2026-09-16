using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITelemetrySchemaService
{
    Task<TelemetrySchema?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetrySchema>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TelemetrySchemaService : ITelemetrySchemaService
{
    private readonly ITelemetrySchemaRepository _repository;

    public TelemetrySchemaService(
        ITelemetrySchemaRepository repository )
    {
        _repository = repository;
    }

    public Task<TelemetrySchema?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TelemetrySchema>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(telemetrySchema, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(telemetrySchema.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a ioTDevice who already has another telemetrySchema.
        if (existing.IoTDeviceId != telemetrySchema.IoTDeviceId)
        {
        }

        existing.SchemaId = telemetrySchema.SchemaId;
        existing.SchemaUri = telemetrySchema.SchemaUri;
        existing.Encoding = telemetrySchema.Encoding;

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
