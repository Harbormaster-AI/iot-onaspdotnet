using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long commanddefinitionId { get; set; }
								 public virtual string name { get; set; }
								