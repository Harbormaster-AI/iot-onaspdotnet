using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IMaintenanceTicketService {

    Task Create(MaintenanceTicket model , CancellationToken cancellationToken);
    Task<bool> Update(MaintenanceTicket model, CancellationToken cancellationToken);
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


    public async Task Create(MaintenanceTicket model, CancellationToken cancellationToken)
    {

 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(MaintenanceTicket model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.TicketNumber = model.TicketNumber;
        existing.OpenedAt = model.OpenedAt;
        existing.ClosedAt = model.ClosedAt;
        existing.Priority = model.Priority;
        existing.Status = model.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<MaintenanceTicket?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<MaintenanceTicket>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
