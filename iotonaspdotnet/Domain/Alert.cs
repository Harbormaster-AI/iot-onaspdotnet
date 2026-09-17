using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Alert
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual string  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual AlertRule  { get; set; }
 public virtual AlertStatus  { get; set; }
}
