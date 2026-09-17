using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
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

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per UsageRecord (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per UsageRecord (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.ConnectivityPlanId).IsRequired();
        // Exactly one ConnectivityPlan per UsageRecord (1:1)
        builder.HasIndex(x => x.ConnectivityPlanId).IsUnique();
    }
}
