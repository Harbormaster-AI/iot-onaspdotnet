using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class DataRetentionPolicyConfiguration : IEntityTypeConfiguration<DataRetentionPolicy>
{
    public void Configure(EntityTypeBuilder<DataRetentionPolicy> builder)
    {
        builder.ToTable("dataRetentionPolicys");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.RetentionDays);

        builder.Property(x => x.Tenant).IsRequired();
        // Exactly one Integer per DataRetentionPolicy (1:1)
        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
