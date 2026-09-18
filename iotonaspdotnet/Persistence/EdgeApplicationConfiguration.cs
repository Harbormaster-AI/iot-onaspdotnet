using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class EdgeApplicationConfiguration : IEntityTypeConfiguration<EdgeApplication>
{
    public void Configure(EntityTypeBuilder<EdgeApplication> builder)
    {
        builder.ToTable("edgeApplications");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Version);
        builder.Property(x => x.Image);
        builder.Property(x => x.DeploymentStatus).HasConversion<string>();

// Exactly one DeploymentStatus per EdgeApplication (1:1)
        builder.Property(x => x.Gateway).IsRequired();
        builder.HasIndex(x => x.Gateway.Id).IsUnique();
    }
}
