using Building.Api.Domain;
using Building.Api.Domain.Enums;
using Building.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Building.Persistence;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("buildings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);

        builder.Property(x => x.SiteId).IsRequired();
        // Exactly one Site per Building (1:1)
        builder.HasIndex(x => x.SiteId).IsUnique();
    }
}
