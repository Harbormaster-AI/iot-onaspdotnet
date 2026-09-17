using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISiteService
{
    Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Site site, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Site site, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class SiteService : ISiteService
{
    private readonly ISiteRepository _repository;
    private readonly ITenantRepository _tenants;

    public SiteService(
        ITenantRepository tenants,
        ISiteRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Site site, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(site.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) site (1:1 relationship).");
        }
        await _repository.AddAsync(site, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Site site, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(site.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another site.
        if (existing.Id != site.Id)
        {
            var Tenant = await _tenants.GetByIdAsync(site.Id, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.Site is not null && Tenant.Site.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an site (1:1 relationship).");
            }
        }

        existing.Name = site.Name;
        existing.Address = site.Address;
        existing.Timezone = site.Timezone;
        existing.Latitude = site.Latitude;
        existing.Longitude = site.Longitude;

        existing.Id = site.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
