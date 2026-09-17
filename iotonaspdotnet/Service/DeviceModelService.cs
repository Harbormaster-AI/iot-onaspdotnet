using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceModelService
{
    Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken);
    Task Create(DeviceModelRequest request , CancellationToken cancellationToken);
    Task<bool> Update(DeviceModelRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class DeviceModelService : IDeviceModelService
{
    private readonly IDeviceModelRepository _repository;

    public DeviceModelService(
        IDeviceModelRepository repository )
    {
        _repository = repository;
    }

    public Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(DeviceModelRequest request, CancellationToken cancellationToken)
    {
        var deviceVendor = await _deviceVendors.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceVendor not found.");

        if (deviceVendor.Vendor is not null)
        {
            throw new InvalidOperationException("DeviceVendor:Vendor already has a(n) DeviceModel (1:1 relationship).");
        }
        var twinTemplate = await _twinTemplates.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("TwinTemplate not found.");

        if (twinTemplate.TwinTemplate is not null)
        {
            throw new InvalidOperationException("TwinTemplate:TwinTemplate already has a(n) DeviceModel (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(DeviceModelRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name
        existing.ModelNumber = request.ModelNumber
        existing.HardwareRevision = request.HardwareRevision
        existing.Vendor = request.Vendor
        existing.HardwareModules = request.HardwareModules
        existing.TwinTemplate = request.TwinTemplate
        existing.FirmwareReleases = request.FirmwareReleases
        existing.CommandDefinitions = request.CommandDefinitions
        existing.SupportedConnectivity = request.SupportedConnectivity
        existing.DefaultTelemetryEncoding = request.DefaultTelemetryEncoding
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = deviceModel.Name;
        existing.ModelNumber = deviceModel.ModelNumber;
        existing.HardwareRevision = deviceModel.HardwareRevision;
        existing.SupportedConnectivity = deviceModel.SupportedConnectivity;
        existing.DefaultTelemetryEncoding = deviceModel.DefaultTelemetryEncoding;

        existing.Id = deviceModel.Id;
        existing.Id = deviceModel.Id;
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
