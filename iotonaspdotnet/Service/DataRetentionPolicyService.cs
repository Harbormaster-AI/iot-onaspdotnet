using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDataRetentionPolicyService
{
    Task<DataRetentionPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataRetentionPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DataRetentionPolicyService : IDataRetentionPolicyService
{
    private readonly IDataRetentionPolicyRepository _repository;
    private readonly ITenantRepository _tenants;

    public DataRetentionPolicyService(
        ITenantRepository tenants,
        IDataRetentionPolicyRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<DataRetentionPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DataRetentionPolicy>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(dataRetentionPolicy.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.DataRetentionPolicy is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) dataRetentionPolicy (1:1 relationship).");
        }

        await _repository.AddAsync(dataRetentionPolicy, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(dataRetentionPolicy.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another dataRetentionPolicy.
        if (existing.Id != dataRetentionPolicy.Id)
        {
            var Tenant = await _tenants.GetByIdAsync(dataRetentionPolicy.Id, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.DataRetentionPolicy is not null && Tenant.DataRetentionPolicy.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an dataRetentionPolicy (1:1 relationship).");
            }
        }

        existing.Name = dataRetentionPolicy.Name;
        existing.RetentionDays = dataRetentionPolicy.RetentionDays;

        existing.Id = dataRetentionPolicy.Id;
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
