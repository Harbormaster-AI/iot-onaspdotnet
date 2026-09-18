using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SoftwareUpdateCampaignRepository : ISoftwareUpdateCampaignRepository
{
    private readonly ApplicationDbContext _db;

    public SoftwareUpdateCampaignRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SoftwareUpdateCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdateCampaigns
            .Include(x => x.FirmwareRelease)
            .Include(x => x.DeviceGroup)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SoftwareUpdateCampaigns
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateCampaigns.Add(softwareUpdateCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateCampaigns.Update(softwareUpdateCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken)
    {
        _db.SoftwareUpdateCampaigns.Remove(softwareUpdateCampaign);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
