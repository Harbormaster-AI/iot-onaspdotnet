using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ISoftwareUpdateCampaignRepository
{
    Task<SoftwareUpdateCampaign?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateCampaign>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken);
    Task UpdateAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken);
    Task DeleteAsync(SoftwareUpdateCampaign softwareUpdateCampaign, CancellationToken cancellationToken);
}
