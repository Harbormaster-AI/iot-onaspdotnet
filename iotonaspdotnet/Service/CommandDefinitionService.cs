
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ICommandDefinitionService {

    Task Create(CommandDefinition model , CancellationToken cancellationToken);
    Task<bool> Update(CommandDefinition model, CancellationToken cancellationToken);
    Task<CommandDefinition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandDefinition>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CommandDefinitionService : ICommandDefinitionService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICommandDefinitionRepository _repository;
    private readonly ILogger<CommandDefinitionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CommandDefinitionService(
        ApplicationTelemetry telemetry,
        ICommandDefinitionRepository repository,
        ILogger<CommandDefinitionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CommandDefinition model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CommandDefinition",
                "CreateCommandDefinition",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CommandDefinition model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.RequestSchemaUri = model.RequestSchemaUri;
            existing.ResponseSchemaUri = model.ResponseSchemaUri;
            existing.TimeoutSeconds = model.TimeoutSeconds;

            await _telemetry.Execute(
                "CommandDefinition",
                "UpdateCommandDefinition",
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

    public Task<CommandDefinition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CommandDefinition>> GetAll(CancellationToken cancellationToken)
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
                "CommandDefinition",
                "UpdateCommandDefinition",
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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CommandDefinition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DeviceModelService>().Get(childRequest, cancellationToken);
            parent.DeviceModel = child;
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

    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CommandDefinition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.DeviceModel = null;
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


    public async Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CommandDefinition",
                "AddToActuators",
                () => _repository.AddToActuatorsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CommandDefinition",
                "RemoveFromActuators",
                () => _repository.RemoveFromActuatorsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CommandDefinition",
                "AddToCommandInvocations",
                () => _repository.AddToCommandInvocationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CommandDefinition",
                "RemoveFromCommandInvocations",
                () => _repository.RemoveFromCommandInvocationsAsync(request, cancellationToken));
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
