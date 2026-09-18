using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class SoftwareUpdateExecutionConfiguration : IEntityTypeConfiguration<SoftwareUpdateExecution>
{
    public void Configure(EntityTypeBuilder<SoftwareUpdateExecution> builder)
    {
        builder.ToTable("SoftwareUpdateExecutions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.StartedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.Status).HasConversion<string>();

// Exactly one UpdateStatus per SoftwareUpdateExecution (1:1)
        builder.Property(x => x.Campaign).IsRequired();
//        builder.HasIndex(x => x.Campaign.Id).IsUnique();
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
    }
}
