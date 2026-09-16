using DeviceGroup.Api.Domain;
using DeviceGroup.Api.Domain.Enums;
using DeviceGroup.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceGroup.Persistence;

public class DeviceGroupConfiguration : IEntityTypeConfiguration<DeviceGroup>
{
    public void Configure(EntityTypeBuilder<DeviceGroup> builder)
    {
        builder.ToTable("deviceGroups");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Criteria);

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per DeviceGroup (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
