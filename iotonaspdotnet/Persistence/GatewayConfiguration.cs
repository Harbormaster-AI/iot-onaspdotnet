using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
{
    public void Configure(EntityTypeBuilder<Gateway> builder)
    {
        builder.ToTable("gateways");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SoftwareVersion);
        builder.Property(x => x.Status).HasConversion<string>();

// Exactly one DeviceStatus per Gateway (1:1)
        builder.Property(x => x.Site).IsRequired();
//        builder.HasIndex(x => x.Site.Id).IsUnique();
        builder.Property(x => x.Room).IsRequired();
//        builder.HasIndex(x => x.Room.Id).IsUnique();
        builder.Property(x => x.DigitalTwin).IsRequired();
//        builder.HasIndex(x => x.DigitalTwin.Id).IsUnique();
    }
}
