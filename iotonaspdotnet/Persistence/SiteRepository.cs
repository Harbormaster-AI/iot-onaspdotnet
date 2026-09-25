
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SiteRepository : ISiteRepository
{
    private readonly ApplicationDbContext _db;

    public SiteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Sites
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Sites
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Add(site);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Update(site);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Site site, CancellationToken cancellationToken)
    {
        _db.Sites.Remove(site);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBuildingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Buildings
            .Where(building =>
                request.ChildIds.Contains(building.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    building =>
                        EF.Property<Guid?>(
                            building,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBuildingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Buildings
            .Where(building =>
                request.ChildIds.Contains(building.Id) &&
                EF.Property<Guid?>(
                    building,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    building =>
                        EF.Property<Guid?>(
                            building,
                            "UsageRecord_Id"),
                    (Guid?)null));
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
