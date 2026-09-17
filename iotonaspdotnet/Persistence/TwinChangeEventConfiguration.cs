using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class TwinChangeEventConfiguration : IEntityTypeConfiguration<TwinChangeEvent>
{
    public void Configure(EntityTypeBuilder<TwinChangeEvent> builder)
    {
        builder.ToTable("twinChangeEvents");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EventId);
        builder.Property(x => x.OccurredAt);
        builder.Property(x => x.TwinChangeType).HasConversion<string>();

        builder.Property(x => x.DigitalTwinId).IsRequired();
        // Exactly one DigitalTwin per TwinChangeEvent (1:1)
        builder.HasIndex(x => x.DigitalTwinId).IsUnique();
    }
}
