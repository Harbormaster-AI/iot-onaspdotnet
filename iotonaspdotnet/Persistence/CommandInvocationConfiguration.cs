using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

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

// Exactly one CommandStatus per CommandInvocation (1:1)
        builder.Property(x => x.Device).IsRequired();
        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.CommandDefinition).IsRequired();
        builder.HasIndex(x => x.CommandDefinition.Id).IsUnique();
        builder.Property(x => x.Actuator).IsRequired();
        builder.HasIndex(x => x.Actuator.Id).IsUnique();
        builder.Property(x => x.User).IsRequired();
        builder.HasIndex(x => x.User.Id).IsUnique();
    }
}
