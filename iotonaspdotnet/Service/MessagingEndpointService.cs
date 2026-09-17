using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IMessagingEndpointService
{
    Task<MessagingEndpoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MessagingEndpoint>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class MessagingEndpointService : IMessagingEndpointService
{
    private readonly IMessagingEndpointRepository _repository;
    private readonly ITenantRepository _tenants;

    public MessagingEndpointService(
        ITenantRepository tenants,
        IMessagingEndpointRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<MessagingEndpoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<MessagingEndpoint>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(messagingEndpoint.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.MessagingEndpoint is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) messagingEndpoint (1:1 relationship).");
        }

        await _repository.AddAsync(messagingEndpoint, cancellationToken);
    }

    public async Task<bool> UpdateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(messagingEndpoint.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another messagingEndpoint.
        if (existing.TenantId != messagingEndpoint.TenantId)
        {
            var Tenant = await _tenants.GetByIdAsync(messagingEndpoint.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.MessagingEndpoint is not null && Tenant.MessagingEndpoint.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an messagingEndpoint (1:1 relationship).");
            }
        }

        existing.Host = messagingEndpoint.Host;
        existing.Port = messagingEndpoint.Port;
        existing.Secure = messagingEndpoint.Secure;
        existing.Protocol = messagingEndpoint.Protocol;

        existing.TenantId = messagingEndpoint.TenantId;
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
