using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
{
    public void Configure(EntityTypeBuilder<IoTDevice> builder)
    {
        builder.ToTable("IoTDevices");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

builder.OwnsOne(x => x.DeviceId, DeviceId =>
{
    DeviceId.Property(x => x.Value).HasColumnName("DeviceId_value");
});
        builder.Property(x => x.SerialNumber);
        builder.Property(x => x.LastSeen);
builder.OwnsOne(x => x.FirmwareVersion, FirmwareVersion =>
{
    FirmwareVersion.Property(x => x.Value).HasColumnName("FirmwareVersion_value");
});
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.PowerSource).HasConversion<string>();

// Exactly one PowerSource per IoTDevice (1:1)
        builder.Property(x => x.DeviceModel).IsRequired();
//        builder.HasIndex(x => x.DeviceModel.Id).IsUnique();
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
        builder.Property(x => x.Site).IsRequired();
//        builder.HasIndex(x => x.Site.Id).IsUnique();
        builder.Property(x => x.Room).IsRequired();
//        builder.HasIndex(x => x.Room.Id).IsUnique();
        builder.Property(x => x.Gateway).IsRequired();
//        builder.HasIndex(x => x.Gateway.Id).IsUnique();
        builder.Property(x => x.DigitalTwin).IsRequired();
//        builder.HasIndex(x => x.DigitalTwin.Id).IsUnique();
        builder.Property(x => x.ProvisioningRecord).IsRequired();
//        builder.HasIndex(x => x.ProvisioningRecord.Id).IsUnique();
    }
}
