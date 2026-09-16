using EdgeApplication.Api.Domain;
using EdgeApplication.Api.Domain.Enums;
using EdgeApplication.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdgeApplication.Persistence;

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

        builder.Property(x => x.GatewayId).IsRequired();
        // Exactly one Gateway per EdgeApplication (1:1)
        builder.HasIndex(x => x.GatewayId).IsUnique();
    }
}
