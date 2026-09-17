using TelemetryStream.Domain;
using TelemetryStream.Domain.Enums;
using TelemetryStream.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TelemetryStream.Persistence;

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

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per TelemetryStream (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.SensorInstanceId).IsRequired();
        // Exactly one SensorInstance per TelemetryStream (1:1)
        builder.HasIndex(x => x.SensorInstanceId).IsUnique();
        builder.Property(x => x.TelemetrySchemaId).IsRequired();
        // Exactly one TelemetrySchema per TelemetryStream (1:1)
        builder.HasIndex(x => x.TelemetrySchemaId).IsUnique();
        builder.Property(x => x.MessagingEndpointId).IsRequired();
        // Exactly one MessagingEndpoint per TelemetryStream (1:1)
        builder.HasIndex(x => x.MessagingEndpointId).IsUnique();
        builder.Property(x => x.DataRetentionPolicyId).IsRequired();
        // Exactly one DataRetentionPolicy per TelemetryStream (1:1)
        builder.HasIndex(x => x.DataRetentionPolicyId).IsUnique();
    }
}
