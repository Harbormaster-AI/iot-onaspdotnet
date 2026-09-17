using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Gateway
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long gatewayId { get; set; }
								 public virtual string softwareVersion { get; set; }
								