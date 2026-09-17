using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceCertificateService
{
    Task<DeviceCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCertificate>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DeviceCertificateService : IDeviceCertificateService
{
    private readonly IDeviceCertificateRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IGatewayRepository _gateways;

    public DeviceCertificateService(
        IIoTDeviceRepository ioTDevices,
        IGatewayRepository gateways,
        IDeviceCertificateRepository repository )
    {
        _repository = repository;
        _ioTDevices = ioTDevices;
        _gateways = gateways;
    }

    public Task<DeviceCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DeviceCertificate>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken)
    {
        var ioTDevice.Device = await _ioTDevices.GetByIdAsync(deviceCertificate.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) deviceCertificate (1:1 relationship).");
        }
        var gateway.Gateway = await _gateways.GetByIdAsync(deviceCertificate.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway already has a(n) deviceCertificate (1:1 relationship).");
        }
        await _repository.AddAsync(deviceCertificate, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(deviceCertificate.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a gateway who already has another deviceCertificate.
        if (existing.Id != deviceCertificate.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(deviceCertificate.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.DeviceCertificate is not null && Device.DeviceCertificate.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an deviceCertificate (1:1 relationship).");
            }
            var Gateway = await _gateways.GetByIdAsync(deviceCertificate.Id, cancellationToken)
                ?? throw new InvalidOperationException("Gateway not found.");

            if (Gateway.DeviceCertificate is not null && Gateway.DeviceCertificate.Id != existing.Id)
            {
                throw new InvalidOperationException("Target gateway already has an deviceCertificate (1:1 relationship).");
            }
        }

        existing.SerialNumber = deviceCertificate.SerialNumber;
        existing.NotBefore = deviceCertificate.NotBefore;
        existing.NotAfter = deviceCertificate.NotAfter;
        existing.Fingerprint = deviceCertificate.Fingerprint;
        existing.CertificateType = deviceCertificate.CertificateType;

        existing.Id = deviceCertificate.Id;
        existing.Id = deviceCertificate.Id;
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
