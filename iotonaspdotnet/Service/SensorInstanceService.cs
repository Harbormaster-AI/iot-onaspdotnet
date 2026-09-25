
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISensorInstanceRepository _repository;
    private readonly ILogger<SensorInstanceService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SensorInstanceService(
        ApplicationTelemetry telemetry,
        ISensorInstanceRepository repository,
        ILogger<SensorInstanceService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(SensorInstance model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "SensorInstance",
                "CreateSensorInstance",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(SensorInstance model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Unit = model.Unit;
            existing.SamplingIntervalMs = model.SamplingIntervalMs;
            existing.SensorType = model.SensorType;

            await _telemetry.Execute(
                "SensorInstance",
                "UpdateSensorInstance",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
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

        try
        {
            await _telemetry.Execute(
                "SensorInstance",
                "UpdateSensorInstance",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SensorInstance found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<IoTDeviceService>().Get(childRequest, cancellationToken);
            parent.Device = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SensorInstance found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Device = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SensorInstance",
                "AddToTelemetryStreams",
                () => _repository.AddToTelemetryStreamsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SensorInstance",
                "RemoveFromTelemetryStreams",
                () => _repository.RemoveFromTelemetryStreamsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
