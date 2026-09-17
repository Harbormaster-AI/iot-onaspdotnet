using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceVendorService
{
    Task<DeviceVendor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceVendor>> GetAll(CancellationToken cancellationToken);
    Task Create(DeviceVendorRequest request , CancellationToken cancellationToken);
    Task<bool> Update(DeviceVendorRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class DeviceVendorService : IDeviceVendorService
{
    private readonly IDeviceVendorRepository _repository;

    public DeviceVendorService(
        IDeviceVendorRepository repository )
    {
        _repository = repository;
    }

    public Task<DeviceVendor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<DeviceVendor>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(DeviceVendorRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(DeviceVendorRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name
        existing.LegalName = request.LegalName
        existing.HeadquartersCountry = request.HeadquartersCountry
        existing.Website = request.Website
        existing.DeviceModels = request.DeviceModels
        existing.FirmwareReleases = request.FirmwareReleases
        existing.HardwareModules = request.HardwareModules
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = deviceVendor.Name;
        existing.LegalName = deviceVendor.LegalName;
        existing.HeadquartersCountry = deviceVendor.HeadquartersCountry;
        existing.Website = deviceVendor.Website;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
