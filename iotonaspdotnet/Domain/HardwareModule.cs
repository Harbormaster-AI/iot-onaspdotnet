using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class HardwareModule
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long hardwaremoduleId { get; set; }
								 public virtual string moduleCode { get; set; }
								