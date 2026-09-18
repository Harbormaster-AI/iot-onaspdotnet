using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ICommandDefinitionRepository
{
    Task<CommandDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandDefinition>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken);
    Task UpdateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken);
    Task DeleteAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken);
}
