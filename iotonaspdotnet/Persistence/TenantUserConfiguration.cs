using TenantUser.Api.Domain;
using TenantUser.Api.Domain.Enums;
using TenantUser.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TenantUser.Persistence;

public class TenantUserConfiguration : IEntityTypeConfiguration<TenantUser>
{
    public void Configure(EntityTypeBuilder<TenantUser> builder)
    {
        builder.ToTable("tenantUsers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.FirstName);
        builder.Property(x => x.LastName);
        builder.Property(x => x.Email);
        builder.Property(x => x.UserRole).HasConversion<string>();

        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per TenantUser (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
