using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DeviceModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual DeviceVendor devicemodelId { get; set; }
								 public virtual DeviceVendor name { get; set; }
								 public virtual DeviceVendor modelNumber { get; set; }
								 public virtual DeviceVendor hardwareRevision { get; set; }
								public virtual DeviceVendor Vendor { get; set; }
								public virtual HardwareModule HardwareModules { get; set; }
								public virtual TwinTemplate TwinTemplate { get; set; }
								public virtual FirmwareRelease FirmwareReleases { get; set; }
								public virtual CommandDefinition CommandDefinitions { get; set; }
								 public virtual CommandDefinition SupportedConnectivity { get; set; }
								 public virtual CommandDefinition DefaultTelemetryEncoding { get; set; }
			}
