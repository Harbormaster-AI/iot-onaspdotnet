using iotonaspdotnet.Domain.DeviceModels;
using iotonaspdotnet.Domain.FirmwareReleases;
using iotonaspdotnet.Domain.HardwareModules;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class DeviceVendor
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long devicevendorId { get; set; }
 public virtual string name { get; set; }
 public virtual string legalName { get; set; }
 public virtual string headquartersCountry { get; set; }
 public virtual string website { get; set; }
public virtual DeviceModel DeviceModels { get; set; }
public virtual FirmwareRelease FirmwareReleases { get; set; }
public virtual HardwareModule HardwareModules { get; set; }
}
