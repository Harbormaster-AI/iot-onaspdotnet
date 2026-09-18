using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class SimCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SimcardId { get; set; } 
 public virtual string? Iccid { get; set; } 
 public virtual string? Imsi { get; set; } 
 public virtual string? Carrier { get; set; } 
public virtual ICollection<NetworkProfile> NetworkProfiles { get; set; } = new List<NetworkProfile>();
public virtual Tenant Tenant { get; set; } 
public virtual ConnectivityPlan ConnectivityPlan { get; set; } 
 public virtual SimStatus? Status { get; set; } 

    public static SimCard FromRequest(SimCardRequest request) {
        return new SimCard {
            Id = request.Id,
            Iccid = request.Iccid,
            Imsi = request.Imsi,
            Carrier = request.Carrier,
            Status = request.Status,
        };
    }
}
