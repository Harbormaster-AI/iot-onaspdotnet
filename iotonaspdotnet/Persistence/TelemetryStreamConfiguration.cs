using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class TelemetryStreamConfiguration : IEntityTypeConfiguration<TelemetryStream>
{
    public void Configure(EntityTypeBuilder<TelemetryStream> builder)
    {
        builder.ToTable("telemetryStreams");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.StreamName);
        builder.Property(x => x.RetentionDays);
        builder.Property(x => x.MessageQoS).HasConversion<string>();

// Exactly one MessageQoS per TelemetryStream (1:1)
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Sensor).IsRequired();
//        builder.HasIndex(x => x.Sensor.Id).IsUnique();
        builder.Property(x => x.Schema).IsRequired();
//        builder.HasIndex(x => x.Schema.Id).IsUnique();
        builder.Property(x => x.MessagingEndpoint).IsRequired();
//        builder.HasIndex(x => x.MessagingEndpoint.Id).IsUnique();
        builder.Property(x => x.RetentionPolicy).IsRequired();
//        builder.HasIndex(x => x.RetentionPolicy.Id).IsUnique();
    }
}
