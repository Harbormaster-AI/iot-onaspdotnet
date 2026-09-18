using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class UsageRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? UsagerecordId { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual int? MessagesSent { get; set; } 
 public virtual int? DataVolumeMB { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual IoTDevice? Device { get; set; } 
public virtual ConnectivityPlan? ConnectivityPlan { get; set; } 

    public static UsageRecord FromRequest(UsageRecordRequest request) {
        return new UsageRecord {
            Id = request.Id,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            MessagesSent = request.MessagesSent,
            DataVolumeMB = request.DataVolumeMB,
        };
    }
}
