using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class TelemetrySchemaConfiguration : IEntityTypeConfiguration<TelemetrySchema>
{
    public void Configure(EntityTypeBuilder<TelemetrySchema> builder)
    {
        builder.ToTable("TelemetrySchemas");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SchemaId);
builder.OwnsOne(x => x.Uri_, SchemaUri =>
{
    SchemaUri.Property(x => x.Value).HasColumnName("SchemaUri_value");
});
        builder.Property(x => x.Encoding).HasConversion<string>();

// Exactly one TelemetryEncoding per TelemetrySchema (1:1)
    }
}
