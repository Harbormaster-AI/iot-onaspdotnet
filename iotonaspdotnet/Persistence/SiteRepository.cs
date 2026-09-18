using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SiteRepository : ISiteRepository
{
    private readonly ApplicationDbContext _db;

    public SiteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Sites
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Sites
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Add(site);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Update(site);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Remove(site);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
