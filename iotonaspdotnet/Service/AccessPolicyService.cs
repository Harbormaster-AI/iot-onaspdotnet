using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IAccessPolicyService
{
    Task<AccessPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccessPolicy>> GetAll(CancellationToken cancellationToken);
    Task Create(AccessPolicyRequest request , CancellationToken cancellationToken);
    Task<bool> Update(AccessPolicyRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class AccessPolicyService : IAccessPolicyService
{
    private readonly IAccessPolicyRepository _repository;

    public AccessPolicyService(
        IAccessPolicyRepository repository )
    {
        _repository = repository;
    }

    public Task<AccessPolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<AccessPolicy>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(AccessPolicyRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) AccessPolicy (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(AccessPolicyRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Scope = request.Scope;
        existing.ExpiresAt = request.ExpiresAt;
        existing.Tenant = request.Tenant;
        existing.ApiKeys = request.ApiKeys;
        existing.Users = request.Users;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = accessPolicy.Name;
        existing.Scope = accessPolicy.Scope;
        existing.ExpiresAt = accessPolicy.ExpiresAt;

        existing.Id = accessPolicy.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
