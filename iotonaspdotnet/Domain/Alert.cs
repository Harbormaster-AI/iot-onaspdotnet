using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Alert
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Alert alertId { get; set; }
								 public virtual Alert raisedAt { get; set; }
								 public virtual Alert clearedAt { get; set; }
								 public virtual Alert message { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual AlertRule AlertRule { get; set; }
								 public virtual AlertRule Status { get; set; }
			}
