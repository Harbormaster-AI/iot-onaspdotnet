using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ApiKey
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TenantUser apikeyId { get; set; }
								 public virtual TenantUser keyId { get; set; }
								 public virtual TenantUser hashedSecret { get; set; }
								 public virtual TenantUser createdAt { get; set; }
								 public virtual TenantUser lastUsedAt { get; set; }
								public virtual AccessPolicy AccessPolicy { get; set; }
			}
