using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
{
    public void Configure(EntityTypeBuilder<IoTDevice> builder)
    {
        builder.ToTable("ioTDevices");
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
        builder.Property(x => x.DeviceStatus).HasConversion<string>();
        builder.Property(x => x.PowerSource).HasConversion<string>();

        builder.Property(x => x.DeviceModelId).IsRequired();
        // Exactly one DeviceModel per IoTDevice (1:1)
        builder.HasIndex(x => x.DeviceModelId).IsUnique();
        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per IoTDevice (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
        builder.Property(x => x.SiteId).IsRequired();
        // Exactly one Site per IoTDevice (1:1)
        builder.HasIndex(x => x.SiteId).IsUnique();
        builder.Property(x => x.RoomId).IsRequired();
        // Exactly one Room per IoTDevice (1:1)
        builder.HasIndex(x => x.RoomId).IsUnique();
        builder.Property(x => x.GatewayId).IsRequired();
        // Exactly one Gateway per IoTDevice (1:1)
        builder.HasIndex(x => x.GatewayId).IsUnique();
        builder.Property(x => x.DigitalTwinId).IsRequired();
        // Exactly one DigitalTwin per IoTDevice (1:1)
        builder.HasIndex(x => x.DigitalTwinId).IsUnique();
        builder.Property(x => x.ProvisioningRecordId).IsRequired();
        // Exactly one ProvisioningRecord per IoTDevice (1:1)
        builder.HasIndex(x => x.ProvisioningRecordId).IsUnique();
    }
}
