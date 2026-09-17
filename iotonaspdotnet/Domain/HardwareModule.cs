using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class HardwareModule
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long hardwaremoduleId { get; set; }
 public virtual string moduleCode { get; set; }
 public virtual Uri datasheetUri { get; set; }
public virtual DeviceVendor Vendor { get; set; }
 public virtual ModuleType ModuleType { get; set; }
}
