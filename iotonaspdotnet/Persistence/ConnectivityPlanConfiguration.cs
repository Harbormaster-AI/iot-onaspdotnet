using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ConnectivityPlanConfiguration : IEntityTypeConfiguration<ConnectivityPlan>
{
    public void Configure(EntityTypeBuilder<ConnectivityPlan> builder)
    {
        builder.ToTable("connectivityPlans");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.DataCapMB);
        builder.Property(x => x.BillingCycleDays);

        builder.Property(x => x.Tenant).IsRequired();
        // Exactly one Integer per ConnectivityPlan (1:1)
        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
