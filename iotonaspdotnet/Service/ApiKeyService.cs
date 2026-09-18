using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IApiKeyService {

    Task Create(ApiKeyRequest request , CancellationToken cancellationToken);
    Task<bool> Update(ApiKeyRequest request, CancellationToken cancellationToken);
    Task<ApiKey?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ApiKey>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken);


}

public class ApiKeyService : IApiKeyService
{
    private readonly IApiKeyRepository _repository;

    public ApiKeyService(
        IApiKeyRepository repository )
    {
        _repository = repository;
    }

    public Task<ApiKey?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<ApiKey>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(ApiKeyRequest request, CancellationToken cancellationToken)
    {
        var accessPolicy = await _accessPolicys.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("AccessPolicy not found.");

        if (accessPolicy.AccessPolicy is not null)
        {
            throw new InvalidOperationException("AccessPolicy:AccessPolicy already has a(n) ApiKey (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(ApiKeyRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.KeyId = request.KeyId;
        existing.HashedSecret = request.HashedSecret;
        existing.CreatedAt = request.CreatedAt;
        existing.LastUsedAt = request.LastUsedAt;
        existing.AccessPolicy = request.AccessPolicy;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    Task<bool> AssignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
