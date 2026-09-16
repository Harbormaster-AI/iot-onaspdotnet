using SoftwareUpdateCampaign.Api.Domain;
using SoftwareUpdateCampaign.Api.Domain.Enums;
using SoftwareUpdateCampaign.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoftwareUpdateCampaign.Persistence;

public class SoftwareUpdateCampaignConfiguration : IEntityTypeConfiguration<SoftwareUpdateCampaign>
{
    public void Configure(EntityTypeBuilder<SoftwareUpdateCampaign> builder)
    {
        builder.ToTable("softwareUpdateCampaigns");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.CampaignCode);
        builder.Property(x => x.ScheduledStart);
        builder.Property(x => x.ScheduledEnd);
        builder.Property(x => x.UpdateCampaignStatus).HasConversion<string>();

        builder.Property(x => x.FirmwareReleaseId).IsRequired();
        // Exactly one FirmwareRelease per SoftwareUpdateCampaign (1:1)
        builder.HasIndex(x => x.FirmwareReleaseId).IsUnique();
        builder.Property(x => x.DeviceGroupId).IsRequired();
        // Exactly one DeviceGroup per SoftwareUpdateCampaign (1:1)
        builder.HasIndex(x => x.DeviceGroupId).IsUnique();
    }
}
