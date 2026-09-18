using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IFirmwareReleaseService {

    Task Create(FirmwareRelease model , CancellationToken cancellationToken);
    Task<bool> Update(FirmwareRelease model, CancellationToken cancellationToken);
    Task<FirmwareRelease?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FirmwareRelease>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);


}

public class FirmwareReleaseService : IFirmwareReleaseService
{
    private readonly IFirmwareReleaseRepository _repository;

    public FirmwareReleaseService(
        IFirmwareReleaseRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(FirmwareRelease model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(FirmwareRelease model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Version = model.Version;
        existing.ReleaseDate = model.ReleaseDate;
        existing.ReleaseNotes = model.ReleaseNotes;
        existing.Checksum = model.Checksum;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<FirmwareRelease?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FirmwareRelease>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
