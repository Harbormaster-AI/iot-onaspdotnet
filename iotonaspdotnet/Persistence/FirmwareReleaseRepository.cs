
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class FirmwareReleaseRepository : IFirmwareReleaseRepository
{
    private readonly ApplicationDbContext _db;

    public FirmwareReleaseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<FirmwareRelease?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FirmwareReleases
            .Include(x => x.DeviceModel)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FirmwareRelease>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.FirmwareReleases
            .AsNoTracking()
            .Include(x => x.DeviceModel)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken)
    {
        _db.FirmwareReleases.Add(firmwareRelease);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken)
    {
        _db.FirmwareReleases.Update(firmwareRelease);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken)
    {
        _db.FirmwareReleases.Remove(firmwareRelease);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
