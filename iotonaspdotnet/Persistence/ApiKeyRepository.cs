using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _db;

    public ApiKeyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ApiKeys
            .Include(x => x.AccessPolicy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ApiKey>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ApiKeys
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ApiKey apiKey, CancellationToken cancellationToken)
    {
        _db.ApiKeys.Add(apiKey);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ApiKey apiKey, CancellationToken cancellationToken)
    {
        _db.ApiKeys.Update(apiKey);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ApiKey apiKey, CancellationToken cancellationToken)
    {
        _db.ApiKeys.Remove(apiKey);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
