using SensorInstance.Domain;
using SensorInstance.Domain.Enums;
using SensorInstance.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SensorInstance.Persistence;

public class SensorInstanceConfiguration : IEntityTypeConfiguration<SensorInstance>
{
    public void Configure(EntityTypeBuilder<SensorInstance> builder)
    {
        builder.ToTable("sensorInstances");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Unit);
        builder.Property(x => x.SamplingIntervalMs);
        builder.Property(x => x.SensorType).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per SensorInstance (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
    }
}
