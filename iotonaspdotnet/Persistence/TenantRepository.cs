
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TenantRepository : ITenantRepository
{
    private readonly ApplicationDbContext _db;

    public TenantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Tenants
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        _db.Tenants.Add(tenant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        _db.Tenants.Update(tenant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        _db.Tenants.Remove(tenant);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToSitesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Sites
            .Where(site =>
                request.ChildIds.Contains(site.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    site =>
                        EF.Property<Guid?>(
                            site,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSitesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Sites
            .Where(site =>
                request.ChildIds.Contains(site.Id) &&
                EF.Property<Guid?>(
                    site,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    site =>
                        EF.Property<Guid?>(
                            site,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TenantUsers
            .Where(tenantUser =>
                request.ChildIds.Contains(tenantUser.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tenantUser =>
                        EF.Property<Guid?>(
                            tenantUser,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TenantUsers
            .Where(tenantUser =>
                request.ChildIds.Contains(tenantUser.Id) &&
                EF.Property<Guid?>(
                    tenantUser,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tenantUser =>
                        EF.Property<Guid?>(
                            tenantUser,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id) &&
                EF.Property<Guid?>(
                    ioTDevice,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToDataRetentionPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataRetentionPolicys
            .Where(dataRetentionPolicy =>
                request.ChildIds.Contains(dataRetentionPolicy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataRetentionPolicy =>
                        EF.Property<Guid?>(
                            dataRetentionPolicy,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDataRetentionPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataRetentionPolicys
            .Where(dataRetentionPolicy =>
                request.ChildIds.Contains(dataRetentionPolicy.Id) &&
                EF.Property<Guid?>(
                    dataRetentionPolicy,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataRetentionPolicy =>
                        EF.Property<Guid?>(
                            dataRetentionPolicy,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToConnectivityPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ConnectivityPlans
            .Where(connectivityPlan =>
                request.ChildIds.Contains(connectivityPlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    connectivityPlan =>
                        EF.Property<Guid?>(
                            connectivityPlan,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromConnectivityPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ConnectivityPlans
            .Where(connectivityPlan =>
                request.ChildIds.Contains(connectivityPlan.Id) &&
                EF.Property<Guid?>(
                    connectivityPlan,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    connectivityPlan =>
                        EF.Property<Guid?>(
                            connectivityPlan,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToSimCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SimCards
            .Where(simCard =>
                request.ChildIds.Contains(simCard.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    simCard =>
                        EF.Property<Guid?>(
                            simCard,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSimCardsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SimCards
            .Where(simCard =>
                request.ChildIds.Contains(simCard.Id) &&
                EF.Property<Guid?>(
                    simCard,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    simCard =>
                        EF.Property<Guid?>(
                            simCard,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToMessagingEndpointsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MessagingEndpoints
            .Where(messagingEndpoint =>
                request.ChildIds.Contains(messagingEndpoint.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    messagingEndpoint =>
                        EF.Property<Guid?>(
                            messagingEndpoint,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMessagingEndpointsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MessagingEndpoints
            .Where(messagingEndpoint =>
                request.ChildIds.Contains(messagingEndpoint.Id) &&
                EF.Property<Guid?>(
                    messagingEndpoint,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    messagingEndpoint =>
                        EF.Property<Guid?>(
                            messagingEndpoint,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToAccessPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AccessPolicys
            .Where(accessPolicy =>
                request.ChildIds.Contains(accessPolicy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    accessPolicy =>
                        EF.Property<Guid?>(
                            accessPolicy,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAccessPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AccessPolicys
            .Where(accessPolicy =>
                request.ChildIds.Contains(accessPolicy.Id) &&
                EF.Property<Guid?>(
                    accessPolicy,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    accessPolicy =>
                        EF.Property<Guid?>(
                            accessPolicy,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToDeviceGroupsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceGroups
            .Where(deviceGroup =>
                request.ChildIds.Contains(deviceGroup.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceGroup =>
                        EF.Property<Guid?>(
                            deviceGroup,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDeviceGroupsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceGroups
            .Where(deviceGroup =>
                request.ChildIds.Contains(deviceGroup.Id) &&
                EF.Property<Guid?>(
                    deviceGroup,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceGroup =>
                        EF.Property<Guid?>(
                            deviceGroup,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToAlertRulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AlertRules
            .Where(alertRule =>
                request.ChildIds.Contains(alertRule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alertRule =>
                        EF.Property<Guid?>(
                            alertRule,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAlertRulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AlertRules
            .Where(alertRule =>
                request.ChildIds.Contains(alertRule.Id) &&
                EF.Property<Guid?>(
                    alertRule,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alertRule =>
                        EF.Property<Guid?>(
                            alertRule,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToMaintenanceTicketsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceTickets
            .Where(maintenanceTicket =>
                request.ChildIds.Contains(maintenanceTicket.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceTicket =>
                        EF.Property<Guid?>(
                            maintenanceTicket,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMaintenanceTicketsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.MaintenanceTickets
            .Where(maintenanceTicket =>
                request.ChildIds.Contains(maintenanceTicket.Id) &&
                EF.Property<Guid?>(
                    maintenanceTicket,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    maintenanceTicket =>
                        EF.Property<Guid?>(
                            maintenanceTicket,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToUsageRecordsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UsageRecords
            .Where(usageRecord =>
                request.ChildIds.Contains(usageRecord.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    usageRecord =>
                        EF.Property<Guid?>(
                            usageRecord,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromUsageRecordsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UsageRecords
            .Where(usageRecord =>
                request.ChildIds.Contains(usageRecord.Id) &&
                EF.Property<Guid?>(
                    usageRecord,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    usageRecord =>
                        EF.Property<Guid?>(
                            usageRecord,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
