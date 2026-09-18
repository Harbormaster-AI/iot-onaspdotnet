using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Gateway
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GatewayId { get; set; } 
 public virtual string? SoftwareVersion { get; set; } 
public virtual Site? Site { get; set; } 
public virtual Room? Room { get; set; } 
public virtual ICollection<IoTDevice> Devices { get; set; } = new List<IoTDevice>();
public virtual ICollection<EdgeApplication> EdgeApplications { get; set; } = new List<EdgeApplication>();
public virtual ICollection<DeviceCertificate> Certificates { get; set; } = new List<DeviceCertificate>();
public virtual DigitalTwin? DigitalTwin { get; set; } 
public virtual ICollection<NetworkProfile> NetworkProfiles { get; set; } = new List<NetworkProfile>();
 public virtual DeviceStatus? Status { get; set; } 

    public static Gateway FromRequest(GatewayRequest request) {
        return new Gateway {
            Id = request.Id,
            SoftwareVersion = request.SoftwareVersion,
            Status = request.Status,
        };
    }
}
