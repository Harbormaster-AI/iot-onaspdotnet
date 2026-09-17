using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

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
        _ioTDevices = ioTDevices;
        _sensorInstances = sensorInstances;
        _telemetrySchemas = telemetrySchemas;
        _messagingEndpoints = messagingEndpoints;
        _dataRetentionPolicys = dataRetentionPolicys;
    }

    public Task<TelemetryStream?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TelemetryStream>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        var ioTDevice.Device = await _ioTDevices.GetByIdAsync(telemetryStream.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) telemetryStream (1:1 relationship).");
        }
        var sensorInstance.Sensor = await _sensorInstances.GetByIdAsync(telemetryStream.Id, cancellationToken)
            ?? throw new InvalidOperationException("SensorInstance not found.");

        if (sensorInstance.Sensor is not null)
        {
            throw new InvalidOperationException("SensorInstance already has a(n) telemetryStream (1:1 relationship).");
        }
        var telemetrySchema.Schema = await _telemetrySchemas.GetByIdAsync(telemetryStream.Id, cancellationToken)
            ?? throw new InvalidOperationException("TelemetrySchema not found.");

        if (telemetrySchema.Schema is not null)
        {
            throw new InvalidOperationException("TelemetrySchema already has a(n) telemetryStream (1:1 relationship).");
        }
        var messagingEndpoint.MessagingEndpoint = await _messagingEndpoints.GetByIdAsync(telemetryStream.Id, cancellationToken)
            ?? throw new InvalidOperationException("MessagingEndpoint not found.");

        if (messagingEndpoint.MessagingEndpoint is not null)
        {
            throw new InvalidOperationException("MessagingEndpoint already has a(n) telemetryStream (1:1 relationship).");
        }
        var dataRetentionPolicy.RetentionPolicy = await _dataRetentionPolicys.GetByIdAsync(telemetryStream.Id, cancellationToken)
            ?? throw new InvalidOperationException("DataRetentionPolicy not found.");

        if (dataRetentionPolicy.RetentionPolicy is not null)
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
        if (existing.Id != telemetryStream.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(telemetryStream.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.TelemetryStream is not null && Device.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an telemetryStream (1:1 relationship).");
            }
            var Sensor = await _sensorInstances.GetByIdAsync(telemetryStream.Id, cancellationToken)
                ?? throw new InvalidOperationException("SensorInstance not found.");

            if (Sensor.TelemetryStream is not null && Sensor.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target sensorInstance already has an telemetryStream (1:1 relationship).");
            }
            var Schema = await _telemetrySchemas.GetByIdAsync(telemetryStream.Id, cancellationToken)
                ?? throw new InvalidOperationException("TelemetrySchema not found.");

            if (Schema.TelemetryStream is not null && Schema.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target telemetrySchema already has an telemetryStream (1:1 relationship).");
            }
            var MessagingEndpoint = await _messagingEndpoints.GetByIdAsync(telemetryStream.Id, cancellationToken)
                ?? throw new InvalidOperationException("MessagingEndpoint not found.");

            if (MessagingEndpoint.TelemetryStream is not null && MessagingEndpoint.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target messagingEndpoint already has an telemetryStream (1:1 relationship).");
            }
            var RetentionPolicy = await _dataRetentionPolicys.GetByIdAsync(telemetryStream.Id, cancellationToken)
                ?? throw new InvalidOperationException("DataRetentionPolicy not found.");

            if (RetentionPolicy.TelemetryStream is not null && RetentionPolicy.TelemetryStream.Id != existing.Id)
            {
                throw new InvalidOperationException("Target dataRetentionPolicy already has an telemetryStream (1:1 relationship).");
            }
        }

        existing.StreamName = telemetryStream.StreamName;
        existing.RetentionDays = telemetryStream.RetentionDays;
        existing.Qos = telemetryStream.Qos;

        existing.Id = telemetryStream.Id;
        existing.Id = telemetryStream.Id;
        existing.Id = telemetryStream.Id;
        existing.Id = telemetryStream.Id;
        existing.Id = telemetryStream.Id;
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
