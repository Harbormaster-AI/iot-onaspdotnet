using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class EdgeApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
public virtual Gateway  { get; set; }
 public virtual DeploymentStatus  { get; set; }
}
