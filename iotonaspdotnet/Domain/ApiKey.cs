using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class ApiKey
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long ApikeyId { get; set; }
 public virtual string KeyId { get; set; }
 public virtual string HashedSecret { get; set; }
 public virtual DateTime CreatedAt { get; set; }
 public virtual DateTime LastUsedAt { get; set; }
public virtual AccessPolicy AccessPolicy { get; set; }

    public static ApiKey FromRequest(ApiKeyRequest request) {
        return new ApiKey {
            Id = request.Id,
            KeyId = request.KeyId,
            HashedSecret = request.HashedSecret,
            CreatedAt = request.CreatedAt,
            LastUsedAt = request.LastUsedAt,
        };
    }
}
