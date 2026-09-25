
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ITelemetrySchemaService {

    Task Create(TelemetrySchema model , CancellationToken cancellationToken);
    Task<bool> Update(TelemetrySchema model, CancellationToken cancellationToken);
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
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITelemetrySchemaRepository _repository;
    private readonly ILogger<TelemetrySchemaService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TelemetrySchemaService(
        ApplicationTelemetry telemetry,
        ITelemetrySchemaRepository repository,
        ILogger<TelemetrySchemaService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(TelemetrySchema model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "TelemetrySchema",
                "CreateTelemetrySchema",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(TelemetrySchema model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.SchemaId = model.SchemaId;
            existing.SchemaUri = model.SchemaUri;
            existing.Encoding = model.Encoding;

            await _telemetry.Execute(
                "TelemetrySchema",
                "UpdateTelemetrySchema",
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

    public Task<TelemetrySchema?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TelemetrySchema>> GetAll(CancellationToken cancellationToken)
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
                "TelemetrySchema",
                "UpdateTelemetrySchema",
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


    public async Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TelemetrySchema",
                "AddToStreams",
                () => _repository.AddToStreamsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TelemetrySchema",
                "RemoveFromStreams",
                () => _repository.RemoveFromStreamsAsync(request, cancellationToken));
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
