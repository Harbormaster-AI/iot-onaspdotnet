using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IApiKeyService {

    Task Create(ApiKey model , CancellationToken cancellationToken);
    Task<bool> Update(ApiKey model, CancellationToken cancellationToken);
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


    public async Task Create(ApiKey model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(ApiKey model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.KeyId = model.KeyId;
        existing.HashedSecret = model.HashedSecret;
        existing.CreatedAt = model.CreatedAt;
        existing.LastUsedAt = model.LastUsedAt;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<ApiKey?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ApiKey>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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

    public async Task<bool> AssignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAccessPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
