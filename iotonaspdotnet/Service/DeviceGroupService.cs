using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IDeviceGroupService {

    Task Create(DeviceGroup model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceGroup model, CancellationToken cancellationToken);
    Task<DeviceGroup?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceGroup>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DeviceGroupService : IDeviceGroupService
{
    private readonly IDeviceGroupRepository _repository;

    public DeviceGroupService(
        IDeviceGroupRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(DeviceGroup model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(DeviceGroup model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.Criteria = model.Criteria;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<DeviceGroup?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceGroup>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
