
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IEdgeApplicationService {

    Task Create(EdgeApplication model , CancellationToken cancellationToken);
    Task<bool> Update(EdgeApplication model, CancellationToken cancellationToken);
    Task<EdgeApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EdgeApplication>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);


}

public class EdgeApplicationService : IEdgeApplicationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IEdgeApplicationRepository _repository;
    private readonly ILogger<EdgeApplicationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public EdgeApplicationService(
        ApplicationTelemetry telemetry,
        IEdgeApplicationRepository repository,
        ILogger<EdgeApplicationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(EdgeApplication model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "EdgeApplication",
                "CreateEdgeApplication",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(EdgeApplication model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Version = model.Version;
            existing.Image = model.Image;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "EdgeApplication",
                "UpdateEdgeApplication",
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

    public Task<EdgeApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EdgeApplication>> GetAll(CancellationToken cancellationToken)
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
                "EdgeApplication",
                "UpdateEdgeApplication",
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

    public async Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No EdgeApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<GatewayService>().Get(childRequest, cancellationToken);
            parent.Gateway = child;
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

    public async Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No EdgeApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Gateway = null;
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




}
