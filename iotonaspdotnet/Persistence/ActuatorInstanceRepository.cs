using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class ActuatorInstanceRepository : IActuatorInstanceRepository
{
    private readonly ApplicationDbContext _db;

    public ActuatorInstanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ActuatorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ActuatorInstances
            .Include(x => x.Device)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ActuatorInstance>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ActuatorInstances
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken)
    {
        _db.ActuatorInstances.Add(actuatorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken)
    {
        _db.ActuatorInstances.Update(actuatorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken)
    {
        _db.ActuatorInstances.Remove(actuatorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
