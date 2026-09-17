using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Gateway
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Gateway gatewayId { get; set; }
								 public virtual Gateway softwareVersion { get; set; }
								public virtual Site Site { get; set; }
								public virtual Room Room { get; set; }
								public virtual IoTDevice Devices { get; set; }
								public virtual EdgeApplication EdgeApplications { get; set; }
								public virtual DeviceCertificate Certificates { get; set; }
								public virtual DigitalTwin DigitalTwin { get; set; }
								public virtual NetworkProfile NetworkProfiles { get; set; }
								 public virtual NetworkProfile Status { get; set; }
			}
