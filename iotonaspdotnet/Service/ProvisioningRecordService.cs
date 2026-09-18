using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IProvisioningRecordService

    Task Create(ProvisioningRecordRequest request , CancellationToken cancellationToken);
    Task<bool> Update(ProvisioningRecordRequest request, CancellationToken cancellationToken);
    Task<ProvisioningRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProvisioningRecord>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCertificate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCertificate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);


}

public class ProvisioningRecordService : IProvisioningRecordService
{
    private readonly IProvisioningRecordRepository _repository;

    public ProvisioningRecordService(
        IProvisioningRecordRepository repository )
    {
        _repository = repository;
    }

    public Task<ProvisioningRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<ProvisioningRecord>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(ProvisioningRecordRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) ProvisioningRecord (1:1 relationship).");
        }
        var deviceCertificate = await _deviceCertificates.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceCertificate not found.");

        if (deviceCertificate.Certificate is not null)
        {
            throw new InvalidOperationException("DeviceCertificate:Certificate already has a(n) ProvisioningRecord (1:1 relationship).");
        }
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) ProvisioningRecord (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(ProvisioningRecordRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.EnrolledAt = request.EnrolledAt;
        existing.ProvisioningService = request.ProvisioningService;
        existing.Device = request.Device;
        existing.Certificate = request.Certificate;
        existing.Tenant = request.Tenant;
        existing.Method = request.Method;
        existing.Status = request.Status;

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

    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignCertificate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignCertificate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
