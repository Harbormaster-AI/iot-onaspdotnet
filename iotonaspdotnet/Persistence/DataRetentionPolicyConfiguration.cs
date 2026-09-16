using DataRetentionPolicy.Api.Domain;
using DataRetentionPolicy.Api.Domain.Enums;
using DataRetentionPolicy.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRetentionPolicy.Persistence;

public class DataRetentionPolicyConfiguration : IEntityTypeConfiguration<DataRetentionPolicy>
{
    public void Configure(EntityTypeBuilder<DataRetentionPolicy> builder)
    {
        builder.ToTable("dataRetentionPolicys");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.RetentionDays);

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per DataRetentionPolicy (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
