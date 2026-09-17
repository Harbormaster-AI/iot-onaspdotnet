using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DeviceModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long devicemodelId { get; set; }
 public virtual string name { get; set; }
 public virtual string modelNumber { get; set; }
 public virtual string hardwareRevision { get; set; }
public virtual DeviceVendor Vendor { get; set; }
public virtual HardwareModule HardwareModules { get; set; }
public virtual TwinTemplate TwinTemplate { get; set; }
public virtual FirmwareRelease FirmwareReleases { get; set; }
public virtual CommandDefinition CommandDefinitions { get; set; }
 public virtual ConnectivityType SupportedConnectivity { get; set; }
 public virtual TelemetryEncoding DefaultTelemetryEncoding { get; set; }
}
