using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ProvisioningRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Gateway provisioningrecordId { get; set; }
								 public virtual Gateway enrolledAt { get; set; }
								 public virtual Gateway provisioningService { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual DeviceCertificate Certificate { get; set; }
								public virtual Tenant Tenant { get; set; }
								 public virtual Tenant Method { get; set; }
								 public virtual Tenant Status { get; set; }
			}
