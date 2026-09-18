using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TenantId { get; set; }
 public virtual string Name { get; set; }
public virtual Site Sites { get; set; }
public virtual TenantUser Users { get; set; }
public virtual IoTDevice Devices { get; set; }
public virtual DataRetentionPolicy DataRetentionPolicies { get; set; }
public virtual ConnectivityPlan ConnectivityPlans { get; set; }
public virtual SimCard SimCards { get; set; }
public virtual MessagingEndpoint MessagingEndpoints { get; set; }
public virtual AccessPolicy AccessPolicies { get; set; }
public virtual DeviceGroup DeviceGroups { get; set; }
public virtual AlertRule AlertRules { get; set; }
public virtual MaintenanceTicket MaintenanceTickets { get; set; }
public virtual UsageRecord UsageRecords { get; set; }
 public virtual TenantType TenantType { get; set; }

    public static Tenant FromRequest(TenantRequest request) {
        return new Tenant {
            Id = model.Id,
            Name = request.Name,
            TenantType = request.TenantType,
        };
}
