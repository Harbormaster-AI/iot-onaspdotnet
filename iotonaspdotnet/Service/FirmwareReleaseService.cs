
using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFirmwareReleaseRepository _repository;
    private readonly ILogger<FirmwareReleaseService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FirmwareReleaseService(
        ApplicationTelemetry telemetry,
        IFirmwareReleaseRepository repository,
        ILogger<FirmwareReleaseService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FirmwareRelease model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FirmwareRelease",
                "CreateFirmwareRelease",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(FirmwareRelease model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Version = model.Version;
            existing.ReleaseDate = model.ReleaseDate;
            existing.ReleaseNotes = model.ReleaseNotes;
            existing.Checksum = model.Checksum;

            await _telemetry.Execute(
                "FirmwareRelease",
                "UpdateFirmwareRelease",
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

        try
        {
            await _telemetry.Execute(
                "FirmwareRelease",
                "UpdateFirmwareRelease",
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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FirmwareRelease found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DeviceModelService>().Get(childRequest, cancellationToken);
            parent.DeviceModel = child;
            await Update( parent, cancellationToken );
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

    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No FirmwareRelease found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.DeviceModel = null;
            await Update( parent, cancellationToken );
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
