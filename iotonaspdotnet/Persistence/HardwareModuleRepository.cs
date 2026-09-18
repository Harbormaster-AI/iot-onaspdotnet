using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class HardwareModuleRepository : IHardwareModuleRepository
{
    private readonly ApplicationDbContext _db;

    public HardwareModuleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<HardwareModule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.HardwareModules
            .Include(x => x.Vendor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<HardwareModule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.HardwareModules
            .AsNoTracking()
            .Include(x => x.Vendor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(HardwareModule hardwareModule, CancellationToken cancellationToken)
    {
        _db.HardwareModules.Add(hardwareModule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken)
    {
        _db.HardwareModules.Update(hardwareModule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HardwareModule hardwareModule, CancellationToken cancellationToken)
    {
        _db.HardwareModules.Remove(hardwareModule);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
