using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class TenantUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TenantuserId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? Email { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual ICollection<CommandInvocation>? CommandInvocations { get; set; } = new List<CommandInvocation>()
 public virtual UserRole? Role { get; set; } 

    public static TenantUser FromRequest(TenantUserRequest request) {
        return new TenantUser {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
        };
    }
}
