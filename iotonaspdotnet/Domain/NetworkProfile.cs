using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class NetworkProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? NetworkprofileId { get; set; }
 public virtual string? ProfileName { get; set; }
 public virtual string? Ssid { get; set; }
 public virtual string? Apn { get; set; }
public virtual IoTDevice? Device { get; set; }
public virtual Gateway? Gateway { get; set; }
public virtual SimCard? SimCard { get; set; }
 public virtual ConnectivityType? ConnectivityType { get; set; }

    public static NetworkProfile FromRequest(NetworkProfileRequest request) {
        return new NetworkProfile {
            Id = request.Id,
            ProfileName = request.ProfileName,
            Ssid = request.Ssid,
            Apn = request.Apn,
            ConnectivityType = request.ConnectivityType,
        };
    }
}
