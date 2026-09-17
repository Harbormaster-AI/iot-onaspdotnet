using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DigitalTwin
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual int  { get; set; }
 public virtual int  { get; set; }
 public virtual DateTime  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual Gateway  { get; set; }
public virtual TwinTemplate  { get; set; }
public virtual TwinChangeEvent  { get; set; }
}
