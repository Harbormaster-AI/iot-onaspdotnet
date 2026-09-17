using Site.Domain;
using Site.Domain.Enums;
using Site.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Site.Persistence;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable("sites");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.Address, Address =>
{
    Address.Property(x => x.Street).HasColumnName("Address_street");
    Address.Property(x => x.City).HasColumnName("Address_city");
    Address.Property(x => x.State).HasColumnName("Address_state");
    Address.Property(x => x.PostalCode).HasColumnName("Address_postalCode");
    Address.Property(x => x.Country).HasColumnName("Address_country");
});
        builder.Property(x => x.Timezone);
        builder.Property(x => x.Latitude);
        builder.Property(x => x.Longitude);

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per Site (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
