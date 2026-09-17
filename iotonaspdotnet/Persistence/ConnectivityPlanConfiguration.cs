using ConnectivityPlan.Domain;
using ConnectivityPlan.Domain.Enums;
using ConnectivityPlan.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectivityPlan.Persistence;

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

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per ConnectivityPlan (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
