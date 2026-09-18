using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class DeviceCertificateConfiguration : IEntityTypeConfiguration<DeviceCertificate>
{
    public void Configure(EntityTypeBuilder<DeviceCertificate> builder)
    {
        builder.ToTable("deviceCertificates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SerialNumber);
        builder.Property(x => x.NotBefore);
        builder.Property(x => x.NotAfter);
        builder.Property(x => x.Fingerprint);
        builder.Property(x => x.CertificateType).HasConversion<string>();

// Exactly one CertificateType per DeviceCertificate (1:1)
        builder.Property(x => x.Device).IsRequired();
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Gateway).IsRequired();
        builder.HasIndex(x => x.Gateway.Id).IsUnique();
    }
}
