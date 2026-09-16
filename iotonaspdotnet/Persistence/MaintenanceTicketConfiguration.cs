using MaintenanceTicket.Api.Domain;
using MaintenanceTicket.Api.Domain.Enums;
using MaintenanceTicket.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaintenanceTicket.Persistence;

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
        builder.Property(x => x.MaintenancePriority).HasConversion<string>();
        builder.Property(x => x.MaintenanceStatus).HasConversion<string>();

        builder.Property(x => x.IoTDeviceId).IsRequired();
        // Exactly one IoTDevice per MaintenanceTicket (1:1)
        builder.HasIndex(x => x.IoTDeviceId).IsUnique();
        builder.Property(x => x.TenantId).IsRequired();
        // Exactly one Tenant per MaintenanceTicket (1:1)
        builder.HasIndex(x => x.TenantId).IsUnique();
    }
}
