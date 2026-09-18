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
            .Include(x => x.${$roleName})
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
}
