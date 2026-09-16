using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class MessagingEndpointRepository : IMessagingEndpointRepository
{
    private readonly ApplicationDbContext _db;

    public MessagingEndpointRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<MessagingEndpoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.MessagingEndpoints
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MessagingEndpoint>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.MessagingEndpoints
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken)
    {
        _db.MessagingEndpoints.Add(messagingEndpoint);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken)
    {
        _db.MessagingEndpoints.Update(messagingEndpoint);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken)
    {
        _db.MessagingEndpoints.Remove(messagingEndpoint);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
