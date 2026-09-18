using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class TenantUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TenantuserId { get; set; }
 public virtual string FirstName { get; set; }
 public virtual string LastName { get; set; }
 public virtual string Email { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual CommandInvocation CommandInvocations { get; set; }
 public virtual UserRole Role { get; set; }

    public static TenantUser FromRequest(TenantUserRequest request) {
        return new TenantUser {
            Id = model.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
        };
}
