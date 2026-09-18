using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IHardwareModuleService {

    Task Create(HardwareModule model , CancellationToken cancellationToken);
    Task<bool> Update(HardwareModule model, CancellationToken cancellationToken);
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


    public async Task Create(HardwareModule model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(HardwareModule model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.ModuleCode = model.ModuleCode;
        existing.DatasheetUri = model.DatasheetUri;
        existing.ModuleType = model.ModuleType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<HardwareModule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<HardwareModule>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
