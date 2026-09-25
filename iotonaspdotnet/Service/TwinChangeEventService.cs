
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ITwinChangeEventService {

    Task Create(TwinChangeEvent model , CancellationToken cancellationToken);
    Task<bool> Update(TwinChangeEvent model, CancellationToken cancellationToken);
    Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTwin(AssociationRequest request, CancellationToken cancellationToken);


}

public class TwinChangeEventService : ITwinChangeEventService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITwinChangeEventRepository _repository;
    private readonly ILogger<TwinChangeEventService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TwinChangeEventService(
        ApplicationTelemetry telemetry,
        ITwinChangeEventRepository repository,
        ILogger<TwinChangeEventService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(TwinChangeEvent model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "TwinChangeEvent",
                "CreateTwinChangeEvent",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(TwinChangeEvent model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EventId = model.EventId;
            existing.OccurredAt = model.OccurredAt;
            existing.ChangeType = model.ChangeType;

            await _telemetry.Execute(
                "TwinChangeEvent",
                "UpdateTwinChangeEvent",
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

    public Task<TwinChangeEvent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TwinChangeEvent>> GetAll(CancellationToken cancellationToken)
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
                "TwinChangeEvent",
                "UpdateTwinChangeEvent",
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

    public async Task<bool> AssignTwin(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No TwinChangeEvent found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DigitalTwinService>().Get(childRequest, cancellationToken);
            parent.Twin = child;
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

    public async Task<bool> UnassignTwin(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No TwinChangeEvent found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Twin = null;
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




}
