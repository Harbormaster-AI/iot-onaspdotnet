using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IMaintenanceTicketService

    Task Create(MaintenanceTicketRequest request , CancellationToken cancellationToken);
    Task<bool> Update(MaintenanceTicketRequest request, CancellationToken cancellationToken);
    Task<MaintenanceTicket?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceTicket>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);


}

public class MaintenanceTicketService : IMaintenanceTicketService
{
    private readonly IMaintenanceTicketRepository _repository;

    public MaintenanceTicketService(
        IMaintenanceTicketRepository repository )
    {
        _repository = repository;
    }

    public Task<MaintenanceTicket?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<MaintenanceTicket>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(MaintenanceTicketRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) MaintenanceTicket (1:1 relationship).");
        }
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) MaintenanceTicket (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(MaintenanceTicketRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.TicketNumber = request.TicketNumber;
        existing.OpenedAt = request.OpenedAt;
        existing.ClosedAt = request.ClosedAt;
        existing.Device = request.Device;
        existing.Tenant = request.Tenant;
        existing.Priority = request.Priority;
        existing.Status = request.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
