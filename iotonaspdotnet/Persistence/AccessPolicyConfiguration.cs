using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class AccessPolicyConfiguration : IEntityTypeConfiguration<AccessPolicy>
{
    public void Configure(EntityTypeBuilder<AccessPolicy> builder)
    {
        builder.ToTable("accessPolicys");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Scope);
        builder.Property(x => x.ExpiresAt);

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per AccessPolicy (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
