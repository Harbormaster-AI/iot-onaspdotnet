using Alert.Domain;
using Alert.Domain.Enums;
using Alert.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alert.Persistence;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.ToTable("alerts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.RaisedAt);
        builder.Property(x => x.ClearedAt);
        builder.Property(x => x.Message);
        builder.Property(x => x.AlertStatus).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per Alert (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.AlertRuleId).IsRequired();
        // Exactly one AlertRule per Alert (1:1)
        builder.HasIndex(x => x.AlertRuleId).IsUnique();
    }
}
