using Gateway.Domain;
using Gateway.Domain.Enums;
using Gateway.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateway.Persistence;

public class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
{
    public void Configure(EntityTypeBuilder<Gateway> builder)
    {
        builder.ToTable("gateways");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SoftwareVersion);
        builder.Property(x => x.DeviceStatus).HasConversion<string>();

        builder.Property(x => x.SiteId).IsRequired();
        // Exactly one Site per Gateway (1:1)
        builder.HasIndex(x => x.SiteId).IsUnique();
        builder.Property(x => x.RoomId).IsRequired();
        // Exactly one Room per Gateway (1:1)
        builder.HasIndex(x => x.RoomId).IsUnique();
        builder.Property(x => x.DigitalTwinId).IsRequired();
        // Exactly one DigitalTwin per Gateway (1:1)
        builder.HasIndex(x => x.DigitalTwinId).IsUnique();
    }
}
