using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ProvisioningRecordConfiguration : IEntityTypeConfiguration<ProvisioningRecord>
{
    public void Configure(EntityTypeBuilder<ProvisioningRecord> builder)
    {
        builder.ToTable("ProvisioningRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EnrolledAt);
        builder.Property(x => x.ProvisioningService);
        builder.Property(x => x.Method).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();

// Exactly one ProvisioningStatus per ProvisioningRecord (1:1)
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Certificate).IsRequired();
//        builder.HasIndex(x => x.Certificate.Id).IsUnique();
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
