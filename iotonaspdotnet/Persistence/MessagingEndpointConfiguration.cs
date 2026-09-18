using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class MessagingEndpointConfiguration : IEntityTypeConfiguration<MessagingEndpoint>
{
    public void Configure(EntityTypeBuilder<MessagingEndpoint> builder)
    {
        builder.ToTable("messagingEndpoints");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Host);
        builder.Property(x => x.Port);
        builder.Property(x => x.Secure);
        builder.Property(x => x.MessagingProtocol).HasConversion<string>();

// Exactly one MessagingProtocol per MessagingEndpoint (1:1)
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
