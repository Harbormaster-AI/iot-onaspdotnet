using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

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

// Exactly one UpdateCampaignStatus per SoftwareUpdateCampaign (1:1)
        builder.Property(x => x.FirmwareRelease).IsRequired();
//        builder.HasIndex(x => x.FirmwareRelease.Id).IsUnique();
        builder.Property(x => x.DeviceGroup).IsRequired();
//        builder.HasIndex(x => x.DeviceGroup.Id).IsUnique();
    }
}
