using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IProvisioningRecordService
{
    Task<ProvisioningRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProvisioningRecord>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class ProvisioningRecordService : IProvisioningRecordService
{
    private readonly IProvisioningRecordRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IDeviceCertificateRepository _deviceCertificates;
    private readonly ITenantRepository _tenants;

    public ProvisioningRecordService(
        IIoTDeviceRepository ioTDevices,
        IDeviceCertificateRepository deviceCertificates,
        ITenantRepository tenants,
        IProvisioningRecordRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
        _tenants = tenants;
        _tenants = tenants;
    }

    public Task<ProvisioningRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProvisioningRecord>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(provisioningRecord.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.ProvisioningRecord is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) provisioningRecord (1:1 relationship).");
        }

        var deviceCertificate = await _deviceCertificates.GetByIdAsync(provisioningRecord.DeviceCertificateId, cancellationToken)
            ?? throw new InvalidOperationException("DeviceCertificate not found.");

        if (deviceCertificate.ProvisioningRecord is not null)
        {
            throw new InvalidOperationException("DeviceCertificate already has a(n) provisioningRecord (1:1 relationship).");
        }

        var tenant = await _tenants.GetByIdAsync(provisioningRecord.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.ProvisioningRecord is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) provisioningRecord (1:1 relationship).");
        }

        await _repository.AddAsync(provisioningRecord, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(provisioningRecord.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another provisioningRecord.
        if (existing.TenantId != provisioningRecord.TenantId)
        {
            var Device = await _ioTDevices.GetByIdAsync(provisioningRecord.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.ProvisioningRecord is not null && Device.ProvisioningRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an provisioningRecord (1:1 relationship).");
            }
            var Certificate = await _deviceCertificates.GetByIdAsync(provisioningRecord.DeviceCertificateId, cancellationToken)
                ?? throw new InvalidOperationException("DeviceCertificate not found.");

            if (Certificate.ProvisioningRecord is not null && Certificate.ProvisioningRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceCertificate already has an provisioningRecord (1:1 relationship).");
            }
            var Tenant = await _tenants.GetByIdAsync(provisioningRecord.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.ProvisioningRecord is not null && Tenant.ProvisioningRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an provisioningRecord (1:1 relationship).");
            }
        }

        existing.EnrolledAt = provisioningRecord.EnrolledAt;
        existing.ProvisioningService = provisioningRecord.ProvisioningService;
        existing.Method = provisioningRecord.Method;
        existing.Status = provisioningRecord.Status;

        existing.IoTDeviceId = provisioningRecord.IoTDeviceId;
        existing.DeviceCertificateId = provisioningRecord.DeviceCertificateId;
        existing.TenantId = provisioningRecord.TenantId;
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
