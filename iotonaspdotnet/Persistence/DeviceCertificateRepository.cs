using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DeviceCertificateRepository : IDeviceCertificateRepository
{
    private readonly ApplicationDbContext _db;

    public DeviceCertificateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DeviceCertificate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DeviceCertificates
            .Include(x => x.Device)
            .Include(x => x.Gateway)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DeviceCertificate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DeviceCertificates
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken)
    {
        _db.DeviceCertificates.Add(deviceCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken)
    {
        _db.DeviceCertificates.Update(deviceCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DeviceCertificate deviceCertificate, CancellationToken cancellationToken)
    {
        _db.DeviceCertificates.Remove(deviceCertificate);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
