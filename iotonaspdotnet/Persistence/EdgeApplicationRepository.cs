using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class EdgeApplicationRepository : IEdgeApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public EdgeApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EdgeApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.EdgeApplications
            .Include(x => x.Gateway)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EdgeApplication>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.EdgeApplications
            .AsNoTracking()
            .Include(x => x.Gateway)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken)
    {
        _db.EdgeApplications.Add(edgeApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (EdgeApplication edgeApplication, CancellationToken cancellationToken)
    {
        _db.EdgeApplications.Update(edgeApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken)
    {
        _db.EdgeApplications.Remove(edgeApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
