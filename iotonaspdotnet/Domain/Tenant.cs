using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
public virtual Site  { get; set; }
public virtual TenantUser  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual DataRetentionPolicy  { get; set; }
public virtual ConnectivityPlan  { get; set; }
public virtual SimCard  { get; set; }
public virtual MessagingEndpoint  { get; set; }
public virtual AccessPolicy  { get; set; }
public virtual DeviceGroup  { get; set; }
public virtual AlertRule  { get; set; }
public virtual MaintenanceTicket  { get; set; }
public virtual UsageRecord  { get; set; }
 public virtual TenantType  { get; set; }
}
