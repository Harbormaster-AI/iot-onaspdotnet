using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class UsageRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long usagerecordId { get; set; }
								 public virtual DateOnly periodStart { get; set; }
								 public virtual DateOnly periodEnd { get; set; }
								 public virtual int messagesSent { get; set; }
								 public virtual int dataVolumeMB { get; set; }
								