using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Site
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SiteId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
 public virtual decimal? Latitude { get; set; } 
 public virtual decimal? Longitude { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual ICollection<Building>? Buildings { get; set; } = new List<Building>()
public virtual ICollection<IoTDevice>? Devices { get; set; } = new List<IoTDevice>()
public virtual ICollection<Gateway>? Gateways { get; set; } = new List<Gateway>()

    public static Site FromRequest(SiteRequest request) {
        return new Site {
            Id = request.Id,
            Name = request.Name,
            Address = request.Address,
            Timezone = request.Timezone,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
    }
}
