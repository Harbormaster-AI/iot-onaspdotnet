using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.IoTDevices;
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service

public interface IMaintenanceTicketService
{
    Task<MaintenanceTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceTicket>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class MaintenanceTicketService : IMaintenanceTicketService
{
    private readonly IMaintenanceTicketRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly ITenantRepository _tenants;

    public MaintenanceTicketService(
        IIoTDeviceRepository ioTDevices,
        ITenantRepository tenants,
        IMaintenanceTicketRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
        _tenants = tenants;
    }

    public Task<MaintenanceTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<MaintenanceTicket>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(maintenanceTicket.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.MaintenanceTicket is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) maintenanceTicket (1:1 relationship).");
        }

        var tenant = await _tenants.GetByIdAsync(maintenanceTicket.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.MaintenanceTicket is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) maintenanceTicket (1:1 relationship).");
        }

        await _repository.AddAsync(maintenanceTicket, cancellationToken);
    }

    public async Task<bool> UpdateAsync(MaintenanceTicket maintenanceTicket, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(maintenanceTicket.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another maintenanceTicket.
        if (existing.TenantId != maintenanceTicket.TenantId)
        {
            var target;
            target = await _ioTDevices.GetByIdAsync(maintenanceTicket.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (target.MaintenanceTicket is not null && target.MaintenanceTicket.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an maintenanceTicket (1:1 relationship).");
            }
            target = await _tenants.GetByIdAsync(maintenanceTicket.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (target.MaintenanceTicket is not null && target.MaintenanceTicket.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an maintenanceTicket (1:1 relationship).");
            }
        }

        existing.TicketNumber = maintenanceTicket.TicketNumber;
        existing.OpenedAt = maintenanceTicket.OpenedAt;
        existing.ClosedAt = maintenanceTicket.ClosedAt;
        existing.Priority = maintenanceTicket.Priority;
        existing.Status = maintenanceTicket.Status;

        existing.IoTDeviceId = maintenanceTicket.IoTDeviceId;
        existing.TenantId = maintenanceTicket.TenantId;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
