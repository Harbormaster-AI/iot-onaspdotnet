using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITenantUserService
{
    Task<TenantUser?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TenantUser>> GetAll(CancellationToken cancellationToken);
    Task Create(TenantUserRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TenantUserRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class TenantUserService : ITenantUserService
{
    private readonly ITenantUserRepository _repository;

    public TenantUserService(
        ITenantUserRepository repository )
    {
        _repository = repository;
    }

    public Task<TenantUser?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<TenantUser>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TenantUserRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) TenantUser (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TenantUserRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.FirstName = request.FirstName;
        existing.LastName = request.LastName;
        existing.Email = request.Email;
        existing.Tenant = request.Tenant;
        existing.CommandInvocations = request.CommandInvocations;
        existing.Role = request.Role;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.FirstName = tenantUser.FirstName;
        existing.LastName = tenantUser.LastName;
        existing.Email = tenantUser.Email;
        existing.Role = tenantUser.Role;

        existing.Id = tenantUser.Id;
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
