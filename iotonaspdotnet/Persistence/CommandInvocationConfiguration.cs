using CommandInvocation.Domain;
using CommandInvocation.Domain.Enums;
using CommandInvocation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandInvocation.Persistence;

public class CommandInvocationConfiguration : IEntityTypeConfiguration<CommandInvocation>
{
    public void Configure(EntityTypeBuilder<CommandInvocation> builder)
    {
        builder.ToTable("commandInvocations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.InvocationId);
        builder.Property(x => x.RequestedAt);
        builder.Property(x => x.CompletedAt);
        builder.Property(x => x.CommandStatus).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per CommandInvocation (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.CommandDefinitionId).IsRequired();
        // Exactly one CommandDefinition per CommandInvocation (1:1)
        builder.HasIndex(x => x.CommandDefinitionId).IsUnique();
        builder.Property(x => x.ActuatorInstanceId).IsRequired();
        // Exactly one ActuatorInstance per CommandInvocation (1:1)
        builder.HasIndex(x => x.ActuatorInstanceId).IsUnique();
        builder.Property(x => x.TenantUserId).IsRequired();
        // Exactly one TenantUser per CommandInvocation (1:1)
        builder.HasIndex(x => x.TenantUserId).IsUnique();
    }
}
