
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IDeviceModelService {

    Task Create(DeviceModel model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceModel model, CancellationToken cancellationToken);
    Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DeviceModelService : IDeviceModelService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDeviceModelRepository _repository;
    private readonly ILogger<DeviceModelService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DeviceModelService(
        ApplicationTelemetry telemetry,
        IDeviceModelRepository repository,
        ILogger<DeviceModelService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DeviceModel model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DeviceModel",
                "CreateDeviceModel",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(DeviceModel model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ModelNumber = model.ModelNumber;
            existing.HardwareRevision = model.HardwareRevision;
            existing.SupportedConnectivity = model.SupportedConnectivity;
            existing.DefaultTelemetryEncoding = model.DefaultTelemetryEncoding;

            await _telemetry.Execute(
                "DeviceModel",
                "UpdateDeviceModel",
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

    public Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken)
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
                "DeviceModel",
                "UpdateDeviceModel",
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

    public async Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceModel found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DeviceVendorService>().Get(childRequest, cancellationToken);
            parent.Vendor = child;
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

    public async Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceModel found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Vendor = null;
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

    public async Task<bool> AssignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceModel found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<TwinTemplateService>().Get(childRequest, cancellationToken);
            parent.TwinTemplate = child;
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

    public async Task<bool> UnassignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DeviceModel found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.TwinTemplate = null;
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


    public async Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "AddToHardwareModules",
                () => _repository.AddToHardwareModulesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "RemoveFromHardwareModules",
                () => _repository.RemoveFromHardwareModulesAsync(request, cancellationToken));
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

    public async Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "AddToFirmwareReleases",
                () => _repository.AddToFirmwareReleasesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "RemoveFromFirmwareReleases",
                () => _repository.RemoveFromFirmwareReleasesAsync(request, cancellationToken));
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

    public async Task<bool> AddToCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "AddToCommandDefinitions",
                () => _repository.AddToCommandDefinitionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceModel",
                "RemoveFromCommandDefinitions",
                () => _repository.RemoveFromCommandDefinitionsAsync(request, cancellationToken));
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
