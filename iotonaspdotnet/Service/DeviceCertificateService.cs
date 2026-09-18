using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IDeviceCertificateService {

    Task Create(DeviceCertificate model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceCertificate model, CancellationToken cancellationToken);
    Task<DeviceCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCertificate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);


}

public class DeviceCertificateService : IDeviceCertificateService
{
    private readonly IDeviceCertificateRepository _repository;

    public DeviceCertificateService(
        IDeviceCertificateRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(DeviceCertificate model, CancellationToken cancellationToken)
    {

 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(DeviceCertificate model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.SerialNumber = model.SerialNumber;
        existing.NotBefore = model.NotBefore;
        existing.NotAfter = model.NotAfter;
        existing.Fingerprint = model.Fingerprint;
        existing.CertificateType = model.CertificateType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<DeviceCertificate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceCertificate>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
