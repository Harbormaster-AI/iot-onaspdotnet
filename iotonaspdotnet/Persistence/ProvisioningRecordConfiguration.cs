using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ProvisioningRecordConfiguration : IEntityTypeConfiguration<ProvisioningRecord>
{
    public void Configure(EntityTypeBuilder<ProvisioningRecord> builder)
    {
        builder.ToTable("provisioningRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EnrolledAt);
        builder.Property(x => x.ProvisioningService);
        builder.Property(x => x.ProvisioningMethod).HasConversion<string>();
        builder.Property(x => x.ProvisioningStatus).HasConversion<string>();

        builder.Property(x => x.Device).IsRequired();
        // Exactly one ProvisioningStatus per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Certificate).IsRequired();
        // Exactly one ProvisioningStatus per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.Certificate.Id).IsUnique();
        builder.Property(x => x.Tenant).IsRequired();
        // Exactly one ProvisioningStatus per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
