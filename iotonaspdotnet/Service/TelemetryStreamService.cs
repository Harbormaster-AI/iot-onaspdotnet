using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITelemetryStreamService
{
    Task<TelemetryStream?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetryStream>> GetAll(CancellationToken cancellationToken);
    Task Create(TelemetryStreamRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TelemetryStreamRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class TelemetryStreamService : ITelemetryStreamService
{
    private readonly ITelemetryStreamRepository _repository;

    public TelemetryStreamService(
        ITelemetryStreamRepository repository )
    {
        _repository = repository;
    }

    public Task<TelemetryStream?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<TelemetryStream>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TelemetryStreamRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) TelemetryStream (1:1 relationship).");
        }
        var sensorInstance = await _sensorInstances.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("SensorInstance not found.");

        if (sensorInstance.Sensor is not null)
        {
            throw new InvalidOperationException("SensorInstance:Sensor already has a(n) TelemetryStream (1:1 relationship).");
        }
        var telemetrySchema = await _telemetrySchemas.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("TelemetrySchema not found.");

        if (telemetrySchema.Schema is not null)
        {
            throw new InvalidOperationException("TelemetrySchema:Schema already has a(n) TelemetryStream (1:1 relationship).");
        }
        var messagingEndpoint = await _messagingEndpoints.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("MessagingEndpoint not found.");

        if (messagingEndpoint.MessagingEndpoint is not null)
        {
            throw new InvalidOperationException("MessagingEndpoint:MessagingEndpoint already has a(n) TelemetryStream (1:1 relationship).");
        }
        var dataRetentionPolicy = await _dataRetentionPolicys.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DataRetentionPolicy not found.");

        if (dataRetentionPolicy.RetentionPolicy is not null)
        {
            throw new InvalidOperationException("DataRetentionPolicy:RetentionPolicy already has a(n) TelemetryStream (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TelemetryStreamRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.StreamName = request.StreamName;
        existing.RetentionDays = request.RetentionDays;
        existing.Device = request.Device;
        existing.Sensor = request.Sensor;
        existing.Schema = request.Schema;
        existing.MessagingEndpoint = request.MessagingEndpoint;
        existing.RetentionPolicy = request.RetentionPolicy;
        existing.Qos = request.Qos;

        existing.StreamName = telemetryStream.StreamName;
        existing.RetentionDays = telemetryStream.RetentionDays;
        existing.Qos = telemetryStream.Qos;

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
