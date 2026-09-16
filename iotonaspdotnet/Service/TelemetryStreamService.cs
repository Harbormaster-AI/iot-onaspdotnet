using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.IoTDevices;
using iotonaspdotnet.Persistence.SensorInstances;
using iotonaspdotnet.Persistence.TelemetrySchemas;
using iotonaspdotnet.Persistence.MessagingEndpoints;
using iotonaspdotnet.Persistence.DataRetentionPolicys;

namespace iotonaspdotnet.Service

public interface ITelemetryStreamService
{
    Task<TelemetryStream?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetryStream>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TelemetryStreamService : ITelemetryStreamService
{
    private readonly ITelemetryStreamRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly ISensorInstanceRepository _sensorInstances;
    private readonly ITelemetrySchemaRepository _telemetrySchemas;
    private readonly IMessagingEndpointRepository _messagingEndpoints;
    private readonly IDataRetentionPolicyRepository _dataRetentionPolicys;

    public TelemetryStreamService(
        IIoTDeviceRepository ioTDevices,
        ISensorInstanceRepository sensorInstances,
        ITelemetrySchemaRepository telemetrySchemas,
        IMessagingEndpointRepository messagingEndpoints,
        IDataRetentionPolicyRepository dataRetentionPolicys,
        ITelemetryStreamRepository repository )
    {
        _repository = repository;
        _dataRetentionPolicys = dataRetentionPolicys;
        _dataRetentionPolicys = dataRetentionPolicys;
        _dataRetentionPolicys = dataRetentionPolicys;
        _dataRetentionPolicys = dataRetentionPolicys;
        _dataRetentionPolicys = dataRetentionPolicys;
    }

    public Task<TelemetryStream?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TelemetryStream>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(telemetryStream.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.TelemetryStream is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) telemetryStream (1:1 relationship).");
        }

        var sensorInstance = await _sensorInstances.GetByIdAsync(telemetryStream.SensorInstanceId, cancellationToken)
            ?? throw new InvalidOperationException("SensorInstance not found.");

        if (sensorInstance.TelemetryStream is not null)
        {
            throw new InvalidOperationException("SensorInstance already has a(n) telemetryStream (1:1 relationship).");
        }

        var telemetrySchema = await _telemetrySchemas.GetByIdAsync(telemetryStream.TelemetrySchemaId, cancellationToken)
            ?? throw new InvalidOperationException("TelemetrySchema not found.");

        if (telemetrySchema.TelemetryStream is not null)
        {
            throw new InvalidOperationException("TelemetrySchema already has a(n) telemetryStream (1:1 relationship).");
        }

        var messagingEndpoint = await _messagingEndpoints.GetByIdAsync(telemetryStream.MessagingEndpointId, cancellationToken)
            ?? throw new InvalidOperationException("MessagingEndpoint not found.");

        if (messagingEndpoint.TelemetryStream is not null)
        {
            throw new InvalidOperationException("MessagingEndpoint already has a(n) telemetryStream (1:1 relationship).");
        }

        var dataRetentionPolicy = await _dataRetentionPolicys.GetByIdAsync(telemetryStream.DataRetentionPolicyId, cancellationToken)
            ?? throw new InvalidOperationException("DataRetentionPolicy not found.");

        if (dataRetentionPolicy.TelemetryStream is not null)
        {
            throw new InvalidOperationException("DataRetentionPolicy already has a(n) telemetryStream (1:1 relationship).");
        }

        await _repository.AddAsync(telemetryStream, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(telemetryStream.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a dataRetentionPolicy who already has another telemetryStream.
        if (existing.DataRetentionPolicyId != telemetryStream.DataRetentionPolicyId)
        {
            var target;
            target = await _ioTDevices.GetByIdAsync(telemetryStream.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (target.TelemetryStream is not null && target.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an telemetryStream (1:1 relationship).");
            }

        }
            target = await _sensorInstances.GetByIdAsync(telemetryStream.SensorInstanceId, cancellationToken)
                ?? throw new InvalidOperationException("SensorInstance not found.");

            if (target.TelemetryStream is not null && target.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target sensorInstance already has an telemetryStream (1:1 relationship).");
            }

        }
            target = await _telemetrySchemas.GetByIdAsync(telemetryStream.TelemetrySchemaId, cancellationToken)
                ?? throw new InvalidOperationException("TelemetrySchema not found.");

            if (target.TelemetryStream is not null && target.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target telemetrySchema already has an telemetryStream (1:1 relationship).");
            }

        }
            target = await _messagingEndpoints.GetByIdAsync(telemetryStream.MessagingEndpointId, cancellationToken)
                ?? throw new InvalidOperationException("MessagingEndpoint not found.");

            if (target.TelemetryStream is not null && target.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target messagingEndpoint already has an telemetryStream (1:1 relationship).");
            }

        }
            target = await _dataRetentionPolicys.GetByIdAsync(telemetryStream.DataRetentionPolicyId, cancellationToken)
                ?? throw new InvalidOperationException("DataRetentionPolicy not found.");

            if (target.TelemetryStream is not null && target.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target dataRetentionPolicy already has an telemetryStream (1:1 relationship).");
            }

        }

        existing.attributeName = telemetryStream.attributeName;
        existing.attributeName = telemetryStream.attributeName;
        existing.attributeName = telemetryStream.attributeName;

        existing.IoTDeviceId = telemetryStream.IoTDeviceId;
        existing.SensorInstanceId = telemetryStream.SensorInstanceId;
        existing.TelemetrySchemaId = telemetryStream.TelemetrySchemaId;
        existing.MessagingEndpointId = telemetryStream.MessagingEndpointId;
        existing.DataRetentionPolicyId = telemetryStream.DataRetentionPolicyId;
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
