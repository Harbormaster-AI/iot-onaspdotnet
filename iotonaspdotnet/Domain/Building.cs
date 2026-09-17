using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Building
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long buildingId { get; set; }
								 public virtual string name { get; set; }
								