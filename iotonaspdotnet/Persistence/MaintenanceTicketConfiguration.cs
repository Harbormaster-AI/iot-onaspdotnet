using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iotonaspdotnet.Persistence;

public class MaintenanceTicketConfiguration : IEntityTypeConfiguration<MaintenanceTicket>
{
    public void Configure(EntityTypeBuilder<MaintenanceTicket> builder)
    {
        builder.ToTable("maintenanceTickets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.TicketNumber);
        builder.Property(x => x.OpenedAt);
        builder.Property(x => x.ClosedAt);
        builder.Property(x => x.Priority).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();

// Exactly one MaintenanceStatus per MaintenanceTicket (1:1)
        builder.Property(x => x.Device).IsRequired();
//        builder.HasIndex(x => x.Device.Id).IsUnique();
        builder.Property(x => x.Tenant).IsRequired();
//        builder.HasIndex(x => x.Tenant.Id).IsUnique();
    }
}
