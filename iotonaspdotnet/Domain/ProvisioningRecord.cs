using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ProvisioningRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long provisioningrecordId { get; set; }
								 public virtual DateTime enrolledAt { get; set; }
								 public virtual string provisioningService { get; set; }
								