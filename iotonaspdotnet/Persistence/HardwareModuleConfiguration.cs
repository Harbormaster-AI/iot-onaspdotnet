using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class HardwareModuleConfiguration : IEntityTypeConfiguration<HardwareModule>
{
    public void Configure(EntityTypeBuilder<HardwareModule> builder)
    {
        builder.ToTable("hardwareModules");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ModuleCode);
builder.OwnsOne(x => x.Uri_, DatasheetUri =>
{
    DatasheetUri.Property(x => x.Value).HasColumnName("DatasheetUri_value");
});
        builder.Property(x => x.ModuleType).HasConversion<string>();

// Exactly one ModuleType per HardwareModule (1:1)
        builder.Property(x => x.Vendor).IsRequired();
//        builder.HasIndex(x => x.Vendor.Id).IsUnique();
    }
}
