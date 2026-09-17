using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class DeviceVendorConfiguration : IEntityTypeConfiguration<DeviceVendor>
{
    public void Configure(EntityTypeBuilder<DeviceVendor> builder)
    {
        builder.ToTable("deviceVendors");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.LegalName);
        builder.Property(x => x.HeadquartersCountry);
        builder.Property(x => x.Website);

    }
}
