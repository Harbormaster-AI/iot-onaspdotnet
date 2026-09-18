using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class ConnectivityPlanConfiguration : IEntityTypeConfiguration<ConnectivityPlan>
{
    public void Configure(EntityTypeBuilder<ConnectivityPlan> builder)
    {
        builder.ToTable("ConnectivityPlans");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.DataCapMB);
        builder.Property(x => x.BillingCycleDays);

// Exactly one Integer per ConnectivityPlan (1:1)
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
