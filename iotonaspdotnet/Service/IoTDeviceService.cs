
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IIoTDeviceService {

    Task Create(IoTDevice model , CancellationToken cancellationToken);
    Task<bool> Update(IoTDevice model, CancellationToken cancellationToken);
    Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSensors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSensors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class IoTDeviceService : IIoTDeviceService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IIoTDeviceRepository _repository;
    private readonly ILogger<IoTDeviceService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public IoTDeviceService(
        ApplicationTelemetry telemetry,
        IIoTDeviceRepository repository,
        ILogger<IoTDeviceService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(IoTDevice model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "IoTDevice",
                "CreateIoTDevice",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(IoTDevice model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.DeviceId = model.DeviceId;
            existing.SerialNumber = model.SerialNumber;
            existing.LastSeen = model.LastSeen;
            existing.FirmwareVersion = model.FirmwareVersion;
            existing.Status = model.Status;
            existing.PowerSource = model.PowerSource;

            await _telemetry.Execute(
                "IoTDevice",
                "UpdateIoTDevice",
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

    public Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken)
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
                "IoTDevice",
                "UpdateIoTDevice",
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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DeviceModelService>().Get(childRequest, cancellationToken);
            parent.DeviceModel = child;
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

    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.DeviceModel = null;
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

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ProvisioningRecordService>().Get(childRequest, cancellationToken);
            parent.ProvisioningRecord = child;
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

    public async Task<bool> UnassignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No IoTDevice found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ProvisioningRecord = null;
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


    public async Task<bool> AddToSensors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToSensors",
                () => _repository.AddToSensorsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSensors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromSensors",
                () => _repository.RemoveFromSensorsAsync(request, cancellationToken));
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

    public async Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToActuators",
                () => _repository.AddToActuatorsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromActuators",
                () => _repository.RemoveFromActuatorsAsync(request, cancellationToken));
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
                "IoTDevice",
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
                "IoTDevice",
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

    public async Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToTelemetryStreams",
                () => _repository.AddToTelemetryStreamsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromTelemetryStreams",
                () => _repository.RemoveFromTelemetryStreamsAsync(request, cancellationToken));
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

    public async Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToCommandInvocations",
                () => _repository.AddToCommandInvocationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromCommandInvocations",
                () => _repository.RemoveFromCommandInvocationsAsync(request, cancellationToken));
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

    public async Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToAlerts",
                () => _repository.AddToAlertsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromAlerts",
                () => _repository.RemoveFromAlertsAsync(request, cancellationToken));
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

    public async Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "AddToDeviceGroups",
                () => _repository.AddToDeviceGroupsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "IoTDevice",
                "RemoveFromDeviceGroups",
                () => _repository.RemoveFromDeviceGroupsAsync(request, cancellationToken));
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
                "IoTDevice",
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
                "IoTDevice",
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
