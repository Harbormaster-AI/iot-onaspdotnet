
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

namespace iotonaspdotnet.Service;

public interface IDeviceVendorService {

    Task Create(DeviceVendor model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceVendor model, CancellationToken cancellationToken);
    Task<DeviceVendor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceVendor>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DeviceVendorService : IDeviceVendorService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDeviceVendorRepository _repository;
    private readonly ILogger<DeviceVendorService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DeviceVendorService(
        ApplicationTelemetry telemetry,
        IDeviceVendorRepository repository,
        ILogger<DeviceVendorService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DeviceVendor model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DeviceVendor",
                "CreateDeviceVendor",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(DeviceVendor model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.HeadquartersCountry = model.HeadquartersCountry;
            existing.Website = model.Website;

            await _telemetry.Execute(
                "DeviceVendor",
                "UpdateDeviceVendor",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<DeviceVendor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceVendor>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _telemetry.Execute(
                "DeviceVendor",
                "UpdateDeviceVendor",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "AddToDeviceModels",
                () => _repository.AddToDeviceModelsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromDeviceModels(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "RemoveFromDeviceModels",
                () => _repository.RemoveFromDeviceModelsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "AddToFirmwareReleases",
                () => _repository.AddToFirmwareReleasesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "RemoveFromFirmwareReleases",
                () => _repository.RemoveFromFirmwareReleasesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "AddToHardwareModules",
                () => _repository.AddToHardwareModulesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DeviceVendor",
                "RemoveFromHardwareModules",
                () => _repository.RemoveFromHardwareModulesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
