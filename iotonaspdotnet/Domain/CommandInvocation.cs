using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandInvocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual CommandInvocation commandinvocationId { get; set; }
								 public virtual CommandInvocation invocationId { get; set; }
								 public virtual CommandInvocation requestedAt { get; set; }
								 public virtual CommandInvocation completedAt { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual CommandDefinition CommandDefinition { get; set; }
								public virtual ActuatorInstance Actuator { get; set; }
								public virtual TenantUser User { get; set; }
								 public virtual TenantUser Status { get; set; }
			}
