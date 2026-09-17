using DeviceModel.Domain;
using DeviceModel.Domain.Enums;
using DeviceModel.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceModel.Persistence;

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

        builder.Property(x => x.DeviceVendorId).IsRequired();
        // Exactly one DeviceVendor per DeviceModel (1:1)
        builder.HasIndex(x => x.DeviceVendorId).IsUnique();
        builder.Property(x => x.TwinTemplateId).IsRequired();
        // Exactly one TwinTemplate per DeviceModel (1:1)
        builder.HasIndex(x => x.TwinTemplateId).IsUnique();
    }
}
