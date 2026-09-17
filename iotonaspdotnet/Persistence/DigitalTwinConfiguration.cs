using DigitalTwin.Domain;
using DigitalTwin.Domain.Enums;
using DigitalTwin.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalTwin.Persistence;

public class DigitalTwinConfiguration : IEntityTypeConfiguration<DigitalTwin>
{
    public void Configure(EntityTypeBuilder<DigitalTwin> builder)
    {
        builder.ToTable("digitalTwins");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.TwinId);
        builder.Property(x => x.DesiredStateVersion);
        builder.Property(x => x.ReportedStateVersion);
        builder.Property(x => x.LastSyncAt);

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per DigitalTwin (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.GatewayId).IsRequired();
        // Exactly one Gateway per DigitalTwin (1:1)
        builder.HasIndex(x => x.GatewayId).IsUnique();
        builder.Property(x => x.TwinTemplateId).IsRequired();
        // Exactly one TwinTemplate per DigitalTwin (1:1)
        builder.HasIndex(x => x.TwinTemplateId).IsUnique();
    }
}
