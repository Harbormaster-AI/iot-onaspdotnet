using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ICommandInvocationService {

    Task Create(CommandInvocation model , CancellationToken cancellationToken);
    Task<bool> Update(CommandInvocation model, CancellationToken cancellationToken);
    Task<CommandInvocation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandInvocation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignActuator(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignActuator(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignUser(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignUser(AssociationRequest request, CancellationToken cancellationToken);


}

public class CommandInvocationService : ICommandInvocationService
{
    private readonly ICommandInvocationRepository _repository;
    private readonly ILogger<CommandInvocationService> _logger;

    public CommandInvocationService(
        ICommandInvocationRepository repository, ILogger<CommandInvocationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CommandInvocation model, CancellationToken cancellationToken)
    {

 
 
 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(CommandInvocation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.InvocationId = model.InvocationId;
            existing.RequestedAt = model.RequestedAt;
            existing.CompletedAt = model.CompletedAt;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CommandInvocation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CommandInvocation>> GetAll(CancellationToken cancellationToken)
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignActuator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignActuator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignUser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignUser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
