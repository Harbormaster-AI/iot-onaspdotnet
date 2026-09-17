using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class FirmwareRelease
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual FirmwareVersion  { get; set; }
 public virtual DateOnly  { get; set; }
 public virtual string  { get; set; }
 public virtual Checksum  { get; set; }
public virtual DeviceModel  { get; set; }
}
