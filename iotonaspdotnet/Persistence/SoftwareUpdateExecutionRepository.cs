using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SoftwareUpdateExecutionRepository : ISoftwareUpdateExecutionRepository
{
    private readonly ApplicationDbContext _db;

    public SoftwareUpdateExecutionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SoftwareUpdateExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdateExecutions
            .Include(x => x.Campaign)
            .Include(x => x.Device)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SoftwareUpdateExecution>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdateExecutions
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateExecutions.Add(softwareUpdateExecution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateExecutions.Update(softwareUpdateExecution);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateExecutions.Remove(softwareUpdateExecution);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
