using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IGatewayRepository
{
    Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Gateway gateway, CancellationToken cancellationToken);
    Task UpdateAsync(Gateway gateway, CancellationToken cancellationToken);
    Task DeleteAsync(Gateway gateway, CancellationToken cancellationToken);
}
