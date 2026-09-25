
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IMessagingEndpointService {

    Task Create(MessagingEndpoint model , CancellationToken cancellationToken);
    Task<bool> Update(MessagingEndpoint model, CancellationToken cancellationToken);
    Task<MessagingEndpoint?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MessagingEndpoint>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class MessagingEndpointService : IMessagingEndpointService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IMessagingEndpointRepository _repository;
    private readonly ILogger<MessagingEndpointService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public MessagingEndpointService(
        ApplicationTelemetry telemetry,
        IMessagingEndpointRepository repository,
        ILogger<MessagingEndpointService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(MessagingEndpoint model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "MessagingEndpoint",
                "CreateMessagingEndpoint",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(MessagingEndpoint model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Host = model.Host;
            existing.Port = model.Port;
            existing.Secure = model.Secure;
            existing.Protocol = model.Protocol;

            await _telemetry.Execute(
                "MessagingEndpoint",
                "UpdateMessagingEndpoint",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<MessagingEndpoint?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MessagingEndpoint>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _telemetry.Execute(
                "MessagingEndpoint",
                "UpdateMessagingEndpoint",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MessagingEndpoint found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<TenantService>().Get(childRequest, cancellationToken);
            parent.Tenant = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No MessagingEndpoint found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Tenant = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MessagingEndpoint",
                "AddToStreams",
                () => _repository.AddToStreamsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "MessagingEndpoint",
                "RemoveFromStreams",
                () => _repository.RemoveFromStreamsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
