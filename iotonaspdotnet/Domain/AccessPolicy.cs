using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class AccessPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AccesspolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Scope { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual ICollection<ApiKey>? ApiKeys { get; set; } = new List<ApiKey>()
public virtual ICollection<TenantUser>? Users { get; set; } = new List<TenantUser>()

    public static AccessPolicy FromRequest(AccessPolicyRequest request) {
        return new AccessPolicy {
            Id = request.Id,
            Name = request.Name,
            Scope = request.Scope,
            ExpiresAt = request.ExpiresAt,
        };
    }
}
