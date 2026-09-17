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

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per DeviceCertificate (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.GatewayId).IsRequired();
        // Exactly one Gateway per DeviceCertificate (1:1)
        builder.HasIndex(x => x.GatewayId).IsUnique();
    }
}
