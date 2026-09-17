using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long tenantId { get; set; }
								 public virtual string name { get; set; }
								