
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ITwinTemplateService {

    Task Create(TwinTemplate model , CancellationToken cancellationToken);
    Task<bool> Update(TwinTemplate model, CancellationToken cancellationToken);
    Task<TwinTemplate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinTemplate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TwinTemplateService : ITwinTemplateService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITwinTemplateRepository _repository;
    private readonly ILogger<TwinTemplateService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TwinTemplateService(
        ApplicationTelemetry telemetry,
        ITwinTemplateRepository repository,
        ILogger<TwinTemplateService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(TwinTemplate model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "TwinTemplate",
                "CreateTwinTemplate",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(TwinTemplate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.SchemaUri = model.SchemaUri;
            existing.Version = model.Version;

            await _telemetry.Execute(
                "TwinTemplate",
                "UpdateTwinTemplate",
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

    public Task<TwinTemplate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TwinTemplate>> GetAll(CancellationToken cancellationToken)
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
                "TwinTemplate",
                "UpdateTwinTemplate",
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


    public async Task<bool> AddToDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TwinTemplate",
                "AddToDeviceModels",
                () => _repository.AddToDeviceModelsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "TwinTemplate",
                "RemoveFromDeviceModels",
                () => _repository.RemoveFromDeviceModelsAsync(request, cancellationToken));
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
