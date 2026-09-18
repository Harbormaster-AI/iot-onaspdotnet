using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class DeviceModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DevicemodelId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ModelNumber { get; set; } 
 public virtual string? HardwareRevision { get; set; } 
public virtual DeviceVendor? Vendor { get; set; } 
public virtual ICollection<HardwareModule> HardwareModules { get; set; } = new List<HardwareModule>();
public virtual TwinTemplate? TwinTemplate { get; set; } 
public virtual ICollection<FirmwareRelease> FirmwareReleases { get; set; } = new List<FirmwareRelease>();
public virtual ICollection<CommandDefinition> CommandDefinitions { get; set; } = new List<CommandDefinition>();
 public virtual ConnectivityType? SupportedConnectivity { get; set; } 
 public virtual TelemetryEncoding? DefaultTelemetryEncoding { get; set; } 

    public static DeviceModel FromRequest(DeviceModelRequest request) {
        return new DeviceModel {
            Id = request.Id,
            Name = request.Name,
            ModelNumber = request.ModelNumber,
            HardwareRevision = request.HardwareRevision,
            SupportedConnectivity = request.SupportedConnectivity,
            DefaultTelemetryEncoding = request.DefaultTelemetryEncoding,
        };
    }
}
