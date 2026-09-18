using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class ConnectivityPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConnectivityplanId { get; set; }
 public virtual string? Name { get; set; }
 public virtual int? DataCapMB { get; set; }
 public virtual int? BillingCycleDays { get; set; }
public virtual SimCard? SimCards { get; set; }
public virtual Tenant? Tenant { get; set; }

    public static ConnectivityPlan FromRequest(ConnectivityPlanRequest request) {
        return new ConnectivityPlan {
            Id = request.Id,
            Name = request.Name,
            DataCapMB = request.DataCapMB,
            BillingCycleDays = request.BillingCycleDays,
        };
    }
}
