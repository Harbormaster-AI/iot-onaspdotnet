using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class UsageRecordConfiguration : IEntityTypeConfiguration<UsageRecord>
{
    public void Configure(EntityTypeBuilder<UsageRecord> builder)
    {
        builder.ToTable("usageRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.PeriodStart);
        builder.Property(x => x.PeriodEnd);
        builder.Property(x => x.MessagesSent);
        builder.Property(x => x.DataVolumeMB);

// Exactly one Integer per UsageRecord (1:1)
        builder.Property(x => x.Tenant).IsRequired();
        builder.HasIndex(x => x.Tenant.Id).IsUnique();
        builder.Property(x => x.Device).IsRequired();
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.ConnectivityPlan).IsRequired();
        builder.HasIndex(x => x.ConnectivityPlan.Id).IsUnique();
    }
}
