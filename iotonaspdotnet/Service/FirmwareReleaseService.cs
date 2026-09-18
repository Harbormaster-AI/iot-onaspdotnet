using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IFirmwareReleaseService {

    Task Create(FirmwareReleaseRequest request , CancellationToken cancellationToken);
    Task<bool> Update(FirmwareReleaseRequest request, CancellationToken cancellationToken);
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

    public Task<FirmwareRelease?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<FirmwareRelease>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(FirmwareReleaseRequest request, CancellationToken cancellationToken)
    {
        var deviceModel = await _deviceModels.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.DeviceModel is not null)
        {
            throw new InvalidOperationException("DeviceModel:DeviceModel already has a(n) FirmwareRelease (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(FirmwareReleaseRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Version = request.Version;
        existing.ReleaseDate = request.ReleaseDate;
        existing.ReleaseNotes = request.ReleaseNotes;
        existing.Checksum = request.Checksum;
        existing.DeviceModel = request.DeviceModel;

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

    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
