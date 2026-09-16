using TelemetrySchema.Api.Domain;
using TelemetrySchema.Api.Domain.Enums;
using TelemetrySchema.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TelemetrySchema.Persistence;

public class TelemetrySchemaConfiguration : IEntityTypeConfiguration<TelemetrySchema>
{
    public void Configure(EntityTypeBuilder<TelemetrySchema> builder)
    {
        builder.ToTable("telemetrySchemas");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.SchemaId);
builder.OwnsOne(x => x.Uri, SchemaUri =>
{
    SchemaUri.Property(x => x.Value).HasColumnName("SchemaUri_value");
});
        builder.Property(x => x.TelemetryEncoding).HasConversion<string>();

    }
}
