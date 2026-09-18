using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceCertificateService
{
    Task<DeviceCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCertificate>> GetAll(CancellationToken cancellationToken);
    Task Create(DeviceCertificateRequest request , CancellationToken cancellationToken);
    Task<bool> Update(DeviceCertificateRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class DeviceCertificateService : IDeviceCertificateService
{
    private readonly IDeviceCertificateRepository _repository;

    public DeviceCertificateService(
        IDeviceCertificateRepository repository )
    {
        _repository = repository;
    }

    public Task<DeviceCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<DeviceCertificate>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(DeviceCertificateRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) DeviceCertificate (1:1 relationship).");
        }
        var gateway = await _gateways.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway:Gateway already has a(n) DeviceCertificate (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(DeviceCertificateRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.SerialNumber = request.SerialNumber;
        existing.NotBefore = request.NotBefore;
        existing.NotAfter = request.NotAfter;
        existing.Fingerprint = request.Fingerprint;
        existing.Device = request.Device;
        existing.Gateway = request.Gateway;
        existing.CertificateType = request.CertificateType;

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
