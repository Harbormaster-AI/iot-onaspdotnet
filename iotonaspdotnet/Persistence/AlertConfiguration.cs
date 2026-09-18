using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

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

        builder.Property(x => x.Device).IsRequired();
        // Exactly one AlertStatus per Alert (1:1)
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.AlertRule).IsRequired();
        // Exactly one AlertStatus per Alert (1:1)
        builder.HasIndex(x => x.AlertRule.Id).IsUnique();
    }
}
