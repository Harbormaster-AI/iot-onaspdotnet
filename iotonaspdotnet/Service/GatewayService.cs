
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IGatewayService {

    Task Create(Gateway model , CancellationToken cancellationToken);
    Task<bool> Update(Gateway model, CancellationToken cancellationToken);
    Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class GatewayService : IGatewayService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IGatewayRepository _repository;
    private readonly ILogger<GatewayService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public GatewayService(
        ApplicationTelemetry telemetry,
        IGatewayRepository repository,
        ILogger<GatewayService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Gateway model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Gateway",
                "CreateGateway",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Gateway model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.SoftwareVersion = model.SoftwareVersion;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Gateway",
                "UpdateGateway",
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

    public Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken)
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
                "Gateway",
                "UpdateGateway",
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

    public async Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<SiteService>().Get(childRequest, cancellationToken);
            parent.Site = child;
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

    public async Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Site = null;
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

    public async Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<RoomService>().Get(childRequest, cancellationToken);
            parent.Room = child;
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

    public async Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Room = null;
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

    public async Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DigitalTwinService>().Get(childRequest, cancellationToken);
            parent.DigitalTwin = child;
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

    public async Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Gateway found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.DigitalTwin = null;
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


    public async Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "AddToDevices",
                () => _repository.AddToDevicesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "RemoveFromDevices",
                () => _repository.RemoveFromDevicesAsync(request, cancellationToken));
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

    public async Task<bool> AddToEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "AddToEdgeApplications",
                () => _repository.AddToEdgeApplicationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "RemoveFromEdgeApplications",
                () => _repository.RemoveFromEdgeApplicationsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "AddToCertificates",
                () => _repository.AddToCertificatesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Gateway",
                "RemoveFromCertificates",
                () => _repository.RemoveFromCertificatesAsync(request, cancellationToken));
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
                "Gateway",
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
                "Gateway",
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
