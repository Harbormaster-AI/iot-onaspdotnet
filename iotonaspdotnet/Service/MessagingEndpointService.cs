using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IMessagingEndpointService
{
    Task<MessagingEndpoint?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MessagingEndpoint>> GetAll(CancellationToken cancellationToken);
    Task Create(MessagingEndpointRequest request , CancellationToken cancellationToken);
    Task<bool> Update(MessagingEndpointRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class MessagingEndpointService : IMessagingEndpointService
{
    private readonly IMessagingEndpointRepository _repository;

    public MessagingEndpointService(
        IMessagingEndpointRepository repository )
    {
        _repository = repository;
    }

    public Task<MessagingEndpoint?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<MessagingEndpoint>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(MessagingEndpointRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) MessagingEndpoint (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(MessagingEndpointRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Host = request.Host
        existing.Port = request.Port
        existing.Secure = request.Secure
        existing.Tenant = request.Tenant
        existing.Streams = request.Streams
        existing.Protocol = request.Protocol
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Host = messagingEndpoint.Host;
        existing.Port = messagingEndpoint.Port;
        existing.Secure = messagingEndpoint.Secure;
        existing.Protocol = messagingEndpoint.Protocol;

        existing.Id = messagingEndpoint.Id;
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
