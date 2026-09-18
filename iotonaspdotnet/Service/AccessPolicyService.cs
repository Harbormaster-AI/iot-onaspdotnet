using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IAccessPolicyService {

    Task Create(AccessPolicy model , CancellationToken cancellationToken);
    Task<bool> Update(AccessPolicy model, CancellationToken cancellationToken);
    Task<AccessPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccessPolicy>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToApiKeys(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApiKeys(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AccessPolicyService : IAccessPolicyService
{
    private readonly IAccessPolicyRepository _repository;

    public AccessPolicyService(
        IAccessPolicyRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(AccessPolicy model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(AccessPolicy model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.Scope = model.Scope;
        existing.ExpiresAt = model.ExpiresAt;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<AccessPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AccessPolicy>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToApiKeys(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApiKeys(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
