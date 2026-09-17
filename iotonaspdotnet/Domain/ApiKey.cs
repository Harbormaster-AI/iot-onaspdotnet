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
}
