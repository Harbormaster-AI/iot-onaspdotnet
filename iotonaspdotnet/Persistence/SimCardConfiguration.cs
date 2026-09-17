using SimCard.Domain;
using SimCard.Domain.Enums;
using SimCard.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimCard.Persistence;

public class SimCardConfiguration : IEntityTypeConfiguration<SimCard>
{
    public void Configure(EntityTypeBuilder<SimCard> builder)
    {
        builder.ToTable("simCards");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Iccid);
        builder.Property(x => x.Imsi);
        builder.Property(x => x.Carrier);
        builder.Property(x => x.SimStatus).HasConversion<string>();

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per SimCard (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
        builder.Property(x => x.ConnectivityPlanId).IsRequired();
        // Exactly one ConnectivityPlan per SimCard (1:1)
        builder.HasIndex(x => x.ConnectivityPlanId).IsUnique();
    }
}
