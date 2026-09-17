using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.ToTable("apiKeys");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.KeyId);
        builder.Property(x => x.HashedSecret);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.LastUsedAt);

        builder.Property(x => x.AccessPolicyId).IsRequired();
        // Exactly one AccessPolicy per ApiKey (1:1)
        builder.HasIndex(x => x.AccessPolicyId).IsUnique();
    }
}
