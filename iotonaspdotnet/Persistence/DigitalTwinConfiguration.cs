using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class DigitalTwinConfiguration : IEntityTypeConfiguration<DigitalTwin>
{
    public void Configure(EntityTypeBuilder<DigitalTwin> builder)
    {
        builder.ToTable("digitalTwins");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.TwinId);
        builder.Property(x => x.DesiredStateVersion);
        builder.Property(x => x.ReportedStateVersion);
        builder.Property(x => x.LastSyncAt);

        builder.Property(x => x.Device).IsRequired();
        // Exactly one DateTime per DigitalTwin (1:1)
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Gateway).IsRequired();
        // Exactly one DateTime per DigitalTwin (1:1)
        builder.HasIndex(x => x.Gateway.Id).IsUnique();
        builder.Property(x => x.Template).IsRequired();
        // Exactly one DateTime per DigitalTwin (1:1)
        builder.HasIndex(x => x.Template.Id).IsUnique();
    }
}
