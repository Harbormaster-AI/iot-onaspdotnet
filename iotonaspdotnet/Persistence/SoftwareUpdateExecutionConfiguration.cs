using SoftwareUpdateExecution.Domain;
using SoftwareUpdateExecution.Domain.Enums;
using SoftwareUpdateExecution.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoftwareUpdateExecution.Persistence;

public class SoftwareUpdateExecutionConfiguration : IEntityTypeConfiguration<SoftwareUpdateExecution>
{
    public void Configure(EntityTypeBuilder<SoftwareUpdateExecution> builder)
    {
        builder.ToTable("softwareUpdateExecutions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.StartedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.UpdateStatus).HasConversion<string>();

        builder.Property(x => x.SoftwareUpdateCampaignId).IsRequired();
        // Exactly one SoftwareUpdateCampaign per SoftwareUpdateExecution (1:1)
        builder.HasIndex(x => x.SoftwareUpdateCampaignId).IsUnique();
        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per SoftwareUpdateExecution (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
    }
}
