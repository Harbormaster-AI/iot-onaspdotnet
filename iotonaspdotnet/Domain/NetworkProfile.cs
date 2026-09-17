using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class NetworkProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Gateway networkprofileId { get; set; }
								 public virtual Gateway profileName { get; set; }
								 public virtual Gateway ssid { get; set; }
								 public virtual Gateway apn { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual Gateway Gateway { get; set; }
								public virtual SimCard SimCard { get; set; }
								 public virtual SimCard ConnectivityType { get; set; }
			}
