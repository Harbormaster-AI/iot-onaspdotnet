
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _db;

    public RoomRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Rooms
            .Include(x => x.Floor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Rooms
            .AsNoTracking()
            .Include(x => x.Floor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Update(room);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDevicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.IoTDevices
            .Where(ioTDevice =>
                request.ChildIds.Contains(ioTDevice.Id) &&
                EF.Property<Guid?>(
                    ioTDevice,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    ioTDevice =>
                        EF.Property<Guid?>(
                            ioTDevice,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToGatewaysAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Gateways
            .Where(gateway =>
                request.ChildIds.Contains(gateway.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    gateway =>
                        EF.Property<Guid?>(
                            gateway,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGatewaysAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Gateways
            .Where(gateway =>
                request.ChildIds.Contains(gateway.Id) &&
                EF.Property<Guid?>(
                    gateway,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    gateway =>
                        EF.Property<Guid?>(
                            gateway,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
