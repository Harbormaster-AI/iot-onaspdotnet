using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TwinTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long twintemplateId { get; set; }
								 public virtual string name { get; set; }
								