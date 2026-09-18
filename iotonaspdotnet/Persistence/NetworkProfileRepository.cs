using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class NetworkProfileRepository : INetworkProfileRepository
{
    private readonly ApplicationDbContext _db;

    public NetworkProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<NetworkProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.NetworkProfiles
            .Include(x => x.IoTDevice)
            .Include(x => x.Gateway)
            .Include(x => x.SimCard)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<NetworkProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.NetworkProfiles
            .AsNoTracking()
            .Include(x => x.IoTDevice)
            .Include(x => x.Gateway)
            .Include(x => x.SimCard)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(NetworkProfile networkProfile, CancellationToken cancellationToken)
    {
        _db.NetworkProfiles.Add(networkProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken)
    {
        _db.NetworkProfiles.Update(networkProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(NetworkProfile networkProfile, CancellationToken cancellationToken)
    {
        _db.NetworkProfiles.Remove(networkProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
