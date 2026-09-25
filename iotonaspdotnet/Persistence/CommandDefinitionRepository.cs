
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class CommandDefinitionRepository : ICommandDefinitionRepository
{
    private readonly ApplicationDbContext _db;

    public CommandDefinitionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CommandDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CommandDefinitions
            .Include(x => x.DeviceModel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CommandDefinition>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CommandDefinitions
            .AsNoTracking()
            .Include(x => x.DeviceModel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken)
    {
        _db.CommandDefinitions.Add(commandDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken)
    {
        _db.CommandDefinitions.Update(commandDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken)
    {
        _db.CommandDefinitions.Remove(commandDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToActuatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ActuatorInstances
            .Where(actuatorInstance =>
                request.ChildIds.Contains(actuatorInstance.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    actuatorInstance =>
                        EF.Property<Guid?>(
                            actuatorInstance,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromActuatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ActuatorInstances
            .Where(actuatorInstance =>
                request.ChildIds.Contains(actuatorInstance.Id) &&
                EF.Property<Guid?>(
                    actuatorInstance,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    actuatorInstance =>
                        EF.Property<Guid?>(
                            actuatorInstance,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id) &&
                EF.Property<Guid?>(
                    commandInvocation,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
