using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Site
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual CommandInvocation siteId { get; set; }
								 public virtual CommandInvocation name { get; set; }
								 public virtual CommandInvocation address { get; set; }
								 public virtual CommandInvocation timezone { get; set; }
								 public virtual CommandInvocation latitude { get; set; }
								 public virtual CommandInvocation longitude { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual Building Buildings { get; set; }
								public virtual IoTDevice Devices { get; set; }
								public virtual Gateway Gateways { get; set; }
			}
