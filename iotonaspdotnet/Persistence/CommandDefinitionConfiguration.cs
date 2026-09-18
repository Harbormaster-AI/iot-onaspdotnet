using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class CommandDefinitionConfiguration : IEntityTypeConfiguration<CommandDefinition>
{
    public void Configure(EntityTypeBuilder<CommandDefinition> builder)
    {
        builder.ToTable("CommandDefinitions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.RequestSchemaUri, RequestSchemaUri =>
{
    RequestSchemaUri.Property(x => x.Value).HasColumnName("RequestSchemaUri_value");
});
builder.OwnsOne(x => x.ResponseSchemaUri, ResponseSchemaUri =>
{
    ResponseSchemaUri.Property(x => x.Value).HasColumnName("ResponseSchemaUri_value");
});
        builder.Property(x => x.TimeoutSeconds);

// Exactly one Integer per CommandDefinition (1:1)
        builder.Property(x => x.DeviceModel).IsRequired();
//        builder.HasIndex(x => x.DeviceModel.Id).IsUnique();
    }
}
