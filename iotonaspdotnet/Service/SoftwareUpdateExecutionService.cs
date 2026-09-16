using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.SoftwareUpdateCampaigns;
using iotonaspdotnet.Persistence.IoTDevices;

namespace iotonaspdotnet.Service

public interface ISoftwareUpdateExecutionService
{
    Task<SoftwareUpdateExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateExecution>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class SoftwareUpdateExecutionService : ISoftwareUpdateExecutionService
{
    private readonly ISoftwareUpdateExecutionRepository _repository;
    private readonly ISoftwareUpdateCampaignRepository _softwareUpdateCampaigns;
    private readonly IIoTDeviceRepository _ioTDevices;

    public SoftwareUpdateExecutionService(
        ISoftwareUpdateCampaignRepository softwareUpdateCampaigns,
        IIoTDeviceRepository ioTDevices,
        ISoftwareUpdateExecutionRepository repository )
    {
        _repository = repository;
        _ioTDevices = ioTDevices;
        _ioTDevices = ioTDevices;
    }

    public Task<SoftwareUpdateExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<SoftwareUpdateExecution>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken)
    {
        var softwareUpdateCampaign = await _softwareUpdateCampaigns.GetByIdAsync(softwareUpdateExecution.SoftwareUpdateCampaignId, cancellationToken)
            ?? throw new InvalidOperationException("SoftwareUpdateCampaign not found.");

        if (softwareUpdateCampaign.SoftwareUpdateExecution is not null)
        {
            throw new InvalidOperationException("SoftwareUpdateCampaign already has a(n) softwareUpdateExecution (1:1 relationship).");
        }

        var ioTDevice = await _ioTDevices.GetByIdAsync(softwareUpdateExecution.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.SoftwareUpdateExecution is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) softwareUpdateExecution (1:1 relationship).");
        }

        await _repository.AddAsync(softwareUpdateExecution, cancellationToken);
    }

    public async Task<bool> UpdateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(softwareUpdateExecution.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a ioTDevice who already has another softwareUpdateExecution.
        if (existing.IoTDeviceId != softwareUpdateExecution.IoTDeviceId)
        {
            var target;
            target = await _softwareUpdateCampaigns.GetByIdAsync(softwareUpdateExecution.SoftwareUpdateCampaignId, cancellationToken)
                ?? throw new InvalidOperationException("SoftwareUpdateCampaign not found.");

            if (target.SoftwareUpdateExecution is not null && target.SoftwareUpdateExecution.Id != existing.Id)
            {
                throw new InvalidOperationException("Target softwareUpdateCampaign already has an softwareUpdateExecution (1:1 relationship).");
            }

        }
            target = await _ioTDevices.GetByIdAsync(softwareUpdateExecution.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (target.SoftwareUpdateExecution is not null && target.SoftwareUpdateExecution.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an softwareUpdateExecution (1:1 relationship).");
            }

        }

        existing.attributeName = softwareUpdateExecution.attributeName;
        existing.attributeName = softwareUpdateExecution.attributeName;
        existing.attributeName = softwareUpdateExecution.attributeName;

        existing.SoftwareUpdateCampaignId = softwareUpdateExecution.SoftwareUpdateCampaignId;
        existing.IoTDeviceId = softwareUpdateExecution.IoTDeviceId;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
