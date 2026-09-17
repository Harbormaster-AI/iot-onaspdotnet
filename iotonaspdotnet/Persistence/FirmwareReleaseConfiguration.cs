using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class FirmwareReleaseConfiguration : IEntityTypeConfiguration<FirmwareRelease>
{
    public void Configure(EntityTypeBuilder<FirmwareRelease> builder)
    {
        builder.ToTable("firmwareReleases");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

builder.OwnsOne(x => x.FirmwareVersion, Version =>
{
    Version.Property(x => x.Value).HasColumnName("Version_value");
});
        builder.Property(x => x.ReleaseDate);
        builder.Property(x => x.ReleaseNotes);
builder.OwnsOne(x => x.Checksum, Checksum =>
{
    Checksum.Property(x => x.Algorithm).HasColumnName("Checksum_algorithm");
    Checksum.Property(x => x.Value).HasColumnName("Checksum_value");
});

        builder.Property(x => x.DeviceModelId).IsRequired();
        // Exactly one DeviceModel per FirmwareRelease (1:1)
        builder.HasIndex(x => x.DeviceModelId).IsUnique();
    }
}
