using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

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

        builder.Property(x => x.Tenant).IsRequired();
        // Exactly one SimStatus per SimCard (1:1)
        builder.HasIndex(x => x.Tenant.Id).IsUnique();
        builder.Property(x => x.ConnectivityPlan).IsRequired();
        // Exactly one SimStatus per SimCard (1:1)
        builder.HasIndex(x => x.ConnectivityPlan.Id).IsUnique();
    }
}
