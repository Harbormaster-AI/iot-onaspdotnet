using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class EdgeApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long edgeapplicationId { get; set; }
								 public virtual string name { get; set; }
								 public virtual string version { get; set; }
								 public virtual string image { get; set; }
								