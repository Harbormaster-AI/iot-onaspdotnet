using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class CommandInvocationRepository : ICommandInvocationRepository
{
    private readonly ApplicationDbContext _db;

    public CommandInvocationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CommandInvocation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CommandInvocations
            .Include(x => x.IoTDevice)
            .Include(x => x.CommandDefinition)
            .Include(x => x.ActuatorInstance)
            .Include(x => x.TenantUser)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CommandInvocation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CommandInvocations
            .AsNoTracking()
            .Include(x => x.IoTDevice)
            .Include(x => x.CommandDefinition)
            .Include(x => x.ActuatorInstance)
            .Include(x => x.TenantUser)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken)
    {
        _db.CommandInvocations.Add(commandInvocation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken)
    {
        _db.CommandInvocations.Update(commandInvocation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken)
    {
        _db.CommandInvocations.Remove(commandInvocation);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
