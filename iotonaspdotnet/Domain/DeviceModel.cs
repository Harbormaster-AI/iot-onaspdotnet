using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DeviceModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long devicemodelId { get; set; }
								 public virtual string name { get; set; }
								 public virtual string modelNumber { get; set; }
								 public virtual string hardwareRevision { get; set; }
								