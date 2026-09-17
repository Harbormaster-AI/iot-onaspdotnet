using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class EdgeApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long EdgeapplicationId { get; set; }
 public virtual string Name { get; set; }
 public virtual string Version { get; set; }
 public virtual string Image { get; set; }
public virtual Gateway Gateway { get; set; }
 public virtual DeploymentStatus Status { get; set; }
}
