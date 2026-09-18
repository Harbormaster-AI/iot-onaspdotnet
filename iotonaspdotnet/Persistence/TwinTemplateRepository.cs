using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TwinTemplateRepository : ITwinTemplateRepository
{
    private readonly ApplicationDbContext _db;

    public TwinTemplateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TwinTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TwinTemplates
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TwinTemplate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TwinTemplates
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken)
    {
        _db.TwinTemplates.Add(twinTemplate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken)
    {
        _db.TwinTemplates.Update(twinTemplate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken)
    {
        _db.TwinTemplates.Remove(twinTemplate);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
