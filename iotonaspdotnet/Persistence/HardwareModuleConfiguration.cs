using HardwareModule.Api.Domain;
using HardwareModule.Api.Domain.Enums;
using HardwareModule.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HardwareModule.Persistence;

public class HardwareModuleConfiguration : IEntityTypeConfiguration<HardwareModule>
{
    public void Configure(EntityTypeBuilder<HardwareModule> builder)
    {
        builder.ToTable("hardwareModules");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ModuleCode);
builder.OwnsOne(x => x.Uri, DatasheetUri =>
{
    DatasheetUri.Property(x => x.Value).HasColumnName("DatasheetUri_value");
});
        builder.Property(x => x.ModuleType).HasConversion<string>();

        builder.Property(x => x.DeviceVendorId).IsRequired();
        // Exactly one DeviceVendor per HardwareModule (1:1)
        builder.HasIndex(x => x.DeviceVendorId).IsUnique();
    }
}
