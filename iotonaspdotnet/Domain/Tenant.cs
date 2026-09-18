using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TenantId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual ICollection<Site>? Sites { get; set; } = new List<Site>()
public virtual ICollection<TenantUser>? Users { get; set; } = new List<TenantUser>()
public virtual ICollection<IoTDevice>? Devices { get; set; } = new List<IoTDevice>()
public virtual ICollection<DataRetentionPolicy>? DataRetentionPolicies { get; set; } = new List<DataRetentionPolicy>()
public virtual ICollection<ConnectivityPlan>? ConnectivityPlans { get; set; } = new List<ConnectivityPlan>()
public virtual ICollection<SimCard>? SimCards { get; set; } = new List<SimCard>()
public virtual ICollection<MessagingEndpoint>? MessagingEndpoints { get; set; } = new List<MessagingEndpoint>()
public virtual ICollection<AccessPolicy>? AccessPolicies { get; set; } = new List<AccessPolicy>()
public virtual ICollection<DeviceGroup>? DeviceGroups { get; set; } = new List<DeviceGroup>()
public virtual ICollection<AlertRule>? AlertRules { get; set; } = new List<AlertRule>()
public virtual ICollection<MaintenanceTicket>? MaintenanceTickets { get; set; } = new List<MaintenanceTicket>()
public virtual ICollection<UsageRecord>? UsageRecords { get; set; } = new List<UsageRecord>()
 public virtual TenantType? TenantType { get; set; } 

    public static Tenant FromRequest(TenantRequest request) {
        return new Tenant {
            Id = request.Id,
            Name = request.Name,
            TenantType = request.TenantType,
        };
    }
}
