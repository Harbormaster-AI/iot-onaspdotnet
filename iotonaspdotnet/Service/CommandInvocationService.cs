using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ICommandInvocationService {

    Task Create(CommandInvocationRequest request , CancellationToken cancellationToken);
    Task<bool> Update(CommandInvocationRequest request, CancellationToken cancellationToken);
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

    public CommandInvocationService(
        ICommandInvocationRepository repository )
    {
        _repository = repository;
    }

    public Task<CommandInvocation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<CommandInvocation>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(CommandInvocationRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) CommandInvocation (1:1 relationship).");
        }
        var commandDefinition = await _commandDefinitions.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("CommandDefinition not found.");

        if (commandDefinition.CommandDefinition is not null)
        {
            throw new InvalidOperationException("CommandDefinition:CommandDefinition already has a(n) CommandInvocation (1:1 relationship).");
        }
        var actuatorInstance = await _actuatorInstances.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("ActuatorInstance not found.");

        if (actuatorInstance.Actuator is not null)
        {
            throw new InvalidOperationException("ActuatorInstance:Actuator already has a(n) CommandInvocation (1:1 relationship).");
        }
        var tenantUser = await _tenantUsers.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("TenantUser not found.");

        if (tenantUser.User is not null)
        {
            throw new InvalidOperationException("TenantUser:User already has a(n) CommandInvocation (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(CommandInvocationRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.InvocationId = request.InvocationId;
        existing.RequestedAt = request.RequestedAt;
        existing.CompletedAt = request.CompletedAt;
        existing.Device = request.Device;
        existing.CommandDefinition = request.CommandDefinition;
        existing.Actuator = request.Actuator;
        existing.User = request.User;
        existing.Status = request.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignCommandDefinition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignActuator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignActuator(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignUser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignUser(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
