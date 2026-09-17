using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);

        builder.Property(x => x.FloorId).IsRequired();
        // Exactly one Floor per Room (1:1)
        builder.HasIndex(x => x.FloorId).IsUnique();
    }
}
