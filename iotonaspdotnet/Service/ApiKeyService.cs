using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.AccessPolicys;

namespace iotonaspdotnet.Service;

public interface IApiKeyService
{
    Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ApiKey>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(ApiKey apiKey, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ApiKey apiKey, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class ApiKeyService : IApiKeyService
{
    private readonly IApiKeyRepository _repository;
    private readonly IAccessPolicyRepository _accessPolicys;

    public ApiKeyService(
        IAccessPolicyRepository accessPolicys,
        IApiKeyRepository repository )
    {
        _repository = repository;
        _accessPolicys = accessPolicys;
    }

    public Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ApiKey>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(ApiKey apiKey, CancellationToken cancellationToken)
    {
        var accessPolicy = await _accessPolicys.GetByIdAsync(apiKey.AccessPolicyId, cancellationToken)
            ?? throw new InvalidOperationException("AccessPolicy not found.");

        if (accessPolicy.ApiKey is not null)
        {
            throw new InvalidOperationException("AccessPolicy already has a(n) apiKey (1:1 relationship).");
        }

        await _repository.AddAsync(apiKey, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ApiKey apiKey, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(apiKey.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a accessPolicy who already has another apiKey.
        if (existing.AccessPolicyId != apiKey.AccessPolicyId)
        {
            var AccessPolicy = await _accessPolicys.GetByIdAsync(apiKey.AccessPolicyId, cancellationToken)
                ?? throw new InvalidOperationException("AccessPolicy not found.");

            if (AccessPolicy.ApiKey is not null && AccessPolicy.ApiKey.Id != existing.Id)
            {
                throw new InvalidOperationException("Target accessPolicy already has an apiKey (1:1 relationship).");
            }
        }

        existing.KeyId = apiKey.KeyId;
        existing.HashedSecret = apiKey.HashedSecret;
        existing.CreatedAt = apiKey.CreatedAt;
        existing.LastUsedAt = apiKey.LastUsedAt;

        existing.AccessPolicyId = apiKey.AccessPolicyId;
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
