using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class GatewayRepository : IGatewayRepository
{
    private readonly ApplicationDbContext _db;

    public GatewayRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Gateways
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.DigitalTwin)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Gateways
            .AsNoTracking()
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.DigitalTwin)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Add(gateway);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Update(gateway);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Remove(gateway);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
