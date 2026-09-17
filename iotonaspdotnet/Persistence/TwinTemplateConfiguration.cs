using iotonaspdotnet.Domain;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class TwinTemplateConfiguration : IEntityTypeConfiguration<TwinTemplate>
{
    public void Configure(EntityTypeBuilder<TwinTemplate> builder)
    {
        builder.ToTable("twinTemplates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name);
builder.OwnsOne(x => x.Uri, SchemaUri =>
{
    SchemaUri.Property(x => x.Value).HasColumnName("SchemaUri_value");
});
        builder.Property(x => x.Version);

    }
}
