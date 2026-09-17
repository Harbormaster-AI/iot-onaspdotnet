using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ApiKey
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long apikeyId { get; set; }
								 public virtual string keyId { get; set; }
								 public virtual string hashedSecret { get; set; }
								 public virtual DateTime createdAt { get; set; }
								 public virtual DateTime lastUsedAt { get; set; }
								public virtual AccessPolicy AccessPolicy { get; set; }
			}
