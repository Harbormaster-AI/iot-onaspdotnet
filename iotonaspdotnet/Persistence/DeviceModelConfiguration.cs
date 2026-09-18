using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class DeviceModelConfiguration : IEntityTypeConfiguration<DeviceModel>
{
    public void Configure(EntityTypeBuilder<DeviceModel> builder)
    {
        builder.ToTable("deviceModels");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.ModelNumber);
        builder.Property(x => x.HardwareRevision);
        builder.Property(x => x.ConnectivityType).HasConversion<string>();
        builder.Property(x => x.TelemetryEncoding).HasConversion<string>();

        builder.Property(x => x.Vendor).IsRequired();
        // Exactly one TelemetryEncoding per DeviceModel (1:1)
        builder.HasIndex(x => x.Vendor.Id).IsUnique();
        builder.Property(x => x.TwinTemplate).IsRequired();
        // Exactly one TelemetryEncoding per DeviceModel (1:1)
        builder.HasIndex(x => x.TwinTemplate.Id).IsUnique();
    }
}
