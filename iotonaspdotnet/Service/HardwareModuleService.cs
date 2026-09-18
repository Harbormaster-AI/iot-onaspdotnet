using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IHardwareModuleService {

    Task Create(HardwareModuleRequest request , CancellationToken cancellationToken);
    Task<bool> Update(HardwareModuleRequest request, CancellationToken cancellationToken);
    Task<HardwareModule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<HardwareModule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken);


}

public class HardwareModuleService : IHardwareModuleService
{
    private readonly IHardwareModuleRepository _repository;

    public HardwareModuleService(
        IHardwareModuleRepository repository )
    {
        _repository = repository;
    }

    public Task<HardwareModule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<HardwareModule>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(HardwareModuleRequest request, CancellationToken cancellationToken)
    {
        var deviceVendor = await _deviceVendors.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceVendor not found.");

        if (deviceVendor.Vendor is not null)
        {
            throw new InvalidOperationException("DeviceVendor:Vendor already has a(n) HardwareModule (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(HardwareModuleRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.ModuleCode = request.ModuleCode;
        existing.DatasheetUri = request.DatasheetUri;
        existing.Vendor = request.Vendor;
        existing.ModuleType = request.ModuleType;

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

    Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
