using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class EdgeApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual NetworkProfile edgeapplicationId { get; set; }
								 public virtual NetworkProfile name { get; set; }
								 public virtual NetworkProfile version { get; set; }
								 public virtual NetworkProfile image { get; set; }
								public virtual Gateway Gateway { get; set; }
								 public virtual Gateway Status { get; set; }
			}
