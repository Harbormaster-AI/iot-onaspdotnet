using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class HardwareModule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual Uri_  { get; set; }
public virtual DeviceVendor  { get; set; }
 public virtual ModuleType  { get; set; }
}
