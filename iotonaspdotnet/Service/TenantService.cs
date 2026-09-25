
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface ITenantService {

    Task Create(Tenant model , CancellationToken cancellationToken);
    Task<bool> Update(Tenant model, CancellationToken cancellationToken);
    Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToSites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TenantService : ITenantService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ITenantRepository _repository;
    private readonly ILogger<TenantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public TenantService(
        ApplicationTelemetry telemetry,
        ITenantRepository repository,
        ILogger<TenantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Tenant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Tenant",
                "CreateTenant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Tenant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TenantType = model.TenantType;

            await _telemetry.Execute(
                "Tenant",
                "UpdateTenant",
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

    public Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken)
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
                "Tenant",
                "UpdateTenant",
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


    public async Task<bool> AddToSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToSites",
                () => _repository.AddToSitesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromSites",
                () => _repository.RemoveFromSitesAsync(request, cancellationToken));
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

    public async Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToUsers",
                () => _repository.AddToUsersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromUsers",
                () => _repository.RemoveFromUsersAsync(request, cancellationToken));
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
                "Tenant",
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
                "Tenant",
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

    public async Task<bool> AddToDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToDataRetentionPolicies",
                () => _repository.AddToDataRetentionPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromDataRetentionPolicies",
                () => _repository.RemoveFromDataRetentionPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> AddToConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToConnectivityPlans",
                () => _repository.AddToConnectivityPlansAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromConnectivityPlans",
                () => _repository.RemoveFromConnectivityPlansAsync(request, cancellationToken));
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

    public async Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToSimCards",
                () => _repository.AddToSimCardsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromSimCards",
                () => _repository.RemoveFromSimCardsAsync(request, cancellationToken));
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

    public async Task<bool> AddToMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToMessagingEndpoints",
                () => _repository.AddToMessagingEndpointsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromMessagingEndpoints",
                () => _repository.RemoveFromMessagingEndpointsAsync(request, cancellationToken));
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

    public async Task<bool> AddToAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToAccessPolicies",
                () => _repository.AddToAccessPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromAccessPolicies",
                () => _repository.RemoveFromAccessPoliciesAsync(request, cancellationToken));
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
                "Tenant",
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
                "Tenant",
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

    public async Task<bool> AddToAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToAlertRules",
                () => _repository.AddToAlertRulesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromAlertRules",
                () => _repository.RemoveFromAlertRulesAsync(request, cancellationToken));
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

    public async Task<bool> AddToMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToMaintenanceTickets",
                () => _repository.AddToMaintenanceTicketsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromMaintenanceTickets",
                () => _repository.RemoveFromMaintenanceTicketsAsync(request, cancellationToken));
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

    public async Task<bool> AddToUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "AddToUsageRecords",
                () => _repository.AddToUsageRecordsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Tenant",
                "RemoveFromUsageRecords",
                () => _repository.RemoveFromUsageRecordsAsync(request, cancellationToken));
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
