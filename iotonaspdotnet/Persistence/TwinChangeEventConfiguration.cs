using iotonaspdotnet.Domain;
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

// Exactly one TwinChangeType per TwinChangeEvent (1:1)
        builder.Property(x => x.Twin).IsRequired();
//        builder.HasIndex(x => x.Twin.Id).IsUnique();
    }
}
