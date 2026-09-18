using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

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

// Exactly one SensorType per SensorInstance (1:1)
        builder.Property(x => x.Device).IsRequired();
        builder.HasIndex(x => x.Device.Id).IsUnique();
    }
}
