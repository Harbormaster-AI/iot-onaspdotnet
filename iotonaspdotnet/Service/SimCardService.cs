
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ISimCardService {

    Task Create(SimCard model , CancellationToken cancellationToken);
    Task<bool> Update(SimCard model, CancellationToken cancellationToken);
    Task<SimCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SimCard>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SimCardService : ISimCardService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISimCardRepository _repository;
    private readonly ILogger<SimCardService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SimCardService(
        ApplicationTelemetry telemetry,
        ISimCardRepository repository,
        ILogger<SimCardService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(SimCard model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "SimCard",
                "CreateSimCard",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(SimCard model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Iccid = model.Iccid;
            existing.Imsi = model.Imsi;
            existing.Carrier = model.Carrier;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "SimCard",
                "UpdateSimCard",
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

    public Task<SimCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SimCard>> GetAll(CancellationToken cancellationToken)
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
                "SimCard",
                "UpdateSimCard",
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
            _logger.LogError("No SimCard found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No SimCard found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SimCard found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ConnectivityPlanService>().Get(childRequest, cancellationToken);
            parent.ConnectivityPlan = child;
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

    public async Task<bool> UnassignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SimCard found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ConnectivityPlan = null;
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


    public async Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SimCard",
                "AddToNetworkProfiles",
                () => _repository.AddToNetworkProfilesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "SimCard",
                "RemoveFromNetworkProfiles",
                () => _repository.RemoveFromNetworkProfilesAsync(request, cancellationToken));
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
