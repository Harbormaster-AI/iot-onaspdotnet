using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class CommandDefinitionConfiguration : IEntityTypeConfiguration<CommandDefinition>
{
    public void Configure(EntityTypeBuilder<CommandDefinition> builder)
    {
        builder.ToTable("commandDefinitions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.Uri_, RequestSchemaUri =>
{
    RequestSchemaUri.Property(x => x.Value).HasColumnName("RequestSchemaUri_value");
});
builder.OwnsOne(x => x.Uri_, ResponseSchemaUri =>
{
    ResponseSchemaUri.Property(x => x.Value).HasColumnName("ResponseSchemaUri_value");
});
        builder.Property(x => x.TimeoutSeconds);

        builder.Property(x => x.DeviceModelId).IsRequired();
        // Exactly one DeviceModel per CommandDefinition (1:1)
        builder.HasIndex(x => x.DeviceModelId).IsUnique();
    }
}
