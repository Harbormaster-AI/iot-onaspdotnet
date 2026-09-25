using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IDeviceCertificateRepository
{
    Task<DeviceCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceCertificate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken);


}
