using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class NetworkProfileConfiguration : IEntityTypeConfiguration<NetworkProfile>
{
    public void Configure(EntityTypeBuilder<NetworkProfile> builder)
    {
        builder.ToTable("networkProfiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ProfileName);
        builder.Property(x => x.Ssid);
        builder.Property(x => x.Apn);
        builder.Property(x => x.ConnectivityType).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per NetworkProfile (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.GatewayId).IsRequired();
        // Exactly one Gateway per NetworkProfile (1:1)
        builder.HasIndex(x => x.GatewayId).IsUnique();
        builder.Property(x => x.SimCardId).IsRequired();
        // Exactly one SimCard per NetworkProfile (1:1)
        builder.HasIndex(x => x.SimCardId).IsUnique();
    }
}
