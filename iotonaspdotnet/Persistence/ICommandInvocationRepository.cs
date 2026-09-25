using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ICommandInvocationRepository
{
    Task<CommandInvocation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandInvocation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken);
    Task UpdateAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken);
    Task DeleteAsync(CommandInvocation commandInvocation, CancellationToken cancellationToken);


}
