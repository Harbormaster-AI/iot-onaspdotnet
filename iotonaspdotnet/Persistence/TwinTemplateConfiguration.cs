using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class TwinTemplateConfiguration : IEntityTypeConfiguration<TwinTemplate>
{
    public void Configure(EntityTypeBuilder<TwinTemplate> builder)
    {
        builder.ToTable("TwinTemplates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.SchemaUri, SchemaUri =>
{
    SchemaUri.Property(x => x.Value).HasColumnName("SchemaUri_value");
});
        builder.Property(x => x.Version);

// Exactly one String per TwinTemplate (1:1)
    }
}
