using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class NetworkProfileConfiguration : IEntityTypeConfiguration<NetworkProfile>
{
    public void Configure(EntityTypeBuilder<NetworkProfile> builder)
    {
        builder.ToTable("NetworkProfiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ProfileName);
        builder.Property(x => x.Ssid);
        builder.Property(x => x.Apn);
        builder.Property(x => x.ConnectivityType).HasConversion<string>();

// Exactly one ConnectivityType per NetworkProfile (1:1)
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Gateway).IsRequired();
//        builder.HasIndex(x => x.Gateway.Id).IsUnique();
        builder.Property(x => x.SimCard).IsRequired();
//        builder.HasIndex(x => x.SimCard.Id).IsUnique();
    }
}
