using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class AlertRuleConfiguration : IEntityTypeConfiguration<AlertRule>
{
    public void Configure(EntityTypeBuilder<AlertRule> builder)
    {
        builder.ToTable("alertRules");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
        builder.Property(x => x.Expression);
        builder.Property(x => x.AlertSeverity).HasConversion<string>();

// Exactly one AlertSeverity per AlertRule (1:1)
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
