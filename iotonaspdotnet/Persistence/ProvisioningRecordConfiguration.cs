using ProvisioningRecord.Api.Domain;
using ProvisioningRecord.Api.Domain.Enums;
using ProvisioningRecord.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProvisioningRecord.Persistence;

public class ProvisioningRecordConfiguration : IEntityTypeConfiguration<ProvisioningRecord>
{
    public void Configure(EntityTypeBuilder<ProvisioningRecord> builder)
    {
        builder.ToTable("provisioningRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.EnrolledAt);
        builder.Property(x => x.ProvisioningService);
        builder.Property(x => x.ProvisioningMethod).HasConversion<string>();
        builder.Property(x => x.ProvisioningStatus).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.DeviceCertificateId).IsRequired();
        // Exactly one DeviceCertificate per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.DeviceCertificateId).IsUnique();
        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per ProvisioningRecord (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
