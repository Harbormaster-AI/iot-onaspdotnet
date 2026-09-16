using AlertRule.Api.Domain;
using AlertRule.Api.Domain.Enums;
using AlertRule.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlertRule.Persistence;

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

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per AlertRule (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
