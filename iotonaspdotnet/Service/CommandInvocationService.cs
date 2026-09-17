using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ICommandInvocationService
{
    Task<CommandInvocation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandInvocation>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class CommandInvocationService : ICommandInvocationService
{
    private readonly ICommandInvocationRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly ICommandDefinitionRepository _commandDefinitions;
    private readonly IActuatorInstanceRepository _actuatorInstances;
    private readonly ITenantUserRepository _tenantUsers;

    public CommandInvocationService(
        IIoTDeviceRepository ioTDevices,
        ICommandDefinitionRepository commandDefinitions,
        IActuatorInstanceRepository actuatorInstances,
        ITenantUserRepository tenantUsers,
        ICommandInvocationRepository repository )
    {
        _repository = repository;
        _tenantUsers = tenantUsers;
        _tenantUsers = tenantUsers;
        _tenantUsers = tenantUsers;
        _tenantUsers = tenantUsers;
    }

    public Task<CommandInvocation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<CommandInvocation>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(commandInvocation.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.CommandInvocation is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) commandInvocation (1:1 relationship).");
        }

        var commandDefinition = await _commandDefinitions.GetByIdAsync(commandInvocation.Id, cancellationToken)
            ?? throw new InvalidOperationException("CommandDefinition not found.");

        if (commandDefinition.CommandInvocation is not null)
        {
            throw new InvalidOperationException("CommandDefinition already has a(n) commandInvocation (1:1 relationship).");
        }

        var actuatorInstance = await _actuatorInstances.GetByIdAsync(commandInvocation.Id, cancellationToken)
            ?? throw new InvalidOperationException("ActuatorInstance not found.");

        if (actuatorInstance.CommandInvocation is not null)
        {
            throw new InvalidOperationException("ActuatorInstance already has a(n) commandInvocation (1:1 relationship).");
        }

        var tenantUser = await _tenantUsers.GetByIdAsync(commandInvocation.Id, cancellationToken)
            ?? throw new InvalidOperationException("TenantUser not found.");

        if (tenantUser.CommandInvocation is not null)
        {
            throw new InvalidOperationException("TenantUser already has a(n) commandInvocation (1:1 relationship).");
        }

        await _repository.AddAsync(commandInvocation, cancellationToken);
    }

    public async Task<bool> UpdateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(commandInvocation.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenantUser who already has another commandInvocation.
        if (existing.Id != commandInvocation.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(commandInvocation.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.CommandInvocation is not null && Device.CommandInvocation.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an commandInvocation (1:1 relationship).");
            }
            var CommandDefinition = await _commandDefinitions.GetByIdAsync(commandInvocation.Id, cancellationToken)
                ?? throw new InvalidOperationException("CommandDefinition not found.");

            if (CommandDefinition.CommandInvocation is not null && CommandDefinition.CommandInvocation.Id != existing.Id)
            {
                throw new InvalidOperationException("Target commandDefinition already has an commandInvocation (1:1 relationship).");
            }
            var Actuator = await _actuatorInstances.GetByIdAsync(commandInvocation.Id, cancellationToken)
                ?? throw new InvalidOperationException("ActuatorInstance not found.");

            if (Actuator.CommandInvocation is not null && Actuator.CommandInvocation.Id != existing.Id)
            {
                throw new InvalidOperationException("Target actuatorInstance already has an commandInvocation (1:1 relationship).");
            }
            var User = await _tenantUsers.GetByIdAsync(commandInvocation.Id, cancellationToken)
                ?? throw new InvalidOperationException("TenantUser not found.");

            if (User.CommandInvocation is not null && User.CommandInvocation.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenantUser already has an commandInvocation (1:1 relationship).");
            }
        }

        existing.InvocationId = commandInvocation.InvocationId;
        existing.RequestedAt = commandInvocation.RequestedAt;
        existing.CompletedAt = commandInvocation.CompletedAt;
        existing.Status = commandInvocation.Status;

        existing.Id = commandInvocation.Id;
        existing.Id = commandInvocation.Id;
        existing.Id = commandInvocation.Id;
        existing.Id = commandInvocation.Id;
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
