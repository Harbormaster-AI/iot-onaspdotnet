
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class GatewayRepository : IGatewayRepository
{
    private readonly ApplicationDbContext _db;

    public GatewayRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Gateways
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.DigitalTwin)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Gateways
            .AsNoTracking()
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.DigitalTwin)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Add(gateway);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Update(gateway);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        _db.Gateways.Remove(gateway);
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


    public async Task AddToEdgeApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EdgeApplications
            .Where(edgeApplication =>
                request.ChildIds.Contains(edgeApplication.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    edgeApplication =>
                        EF.Property<Guid?>(
                            edgeApplication,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEdgeApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EdgeApplications
            .Where(edgeApplication =>
                request.ChildIds.Contains(edgeApplication.Id) &&
                EF.Property<Guid?>(
                    edgeApplication,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    edgeApplication =>
                        EF.Property<Guid?>(
                            edgeApplication,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCertificates
            .Where(deviceCertificate =>
                request.ChildIds.Contains(deviceCertificate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCertificate =>
                        EF.Property<Guid?>(
                            deviceCertificate,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCertificates
            .Where(deviceCertificate =>
                request.ChildIds.Contains(deviceCertificate.Id) &&
                EF.Property<Guid?>(
                    deviceCertificate,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCertificate =>
                        EF.Property<Guid?>(
                            deviceCertificate,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToNetworkProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.NetworkProfiles
            .Where(networkProfile =>
                request.ChildIds.Contains(networkProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    networkProfile =>
                        EF.Property<Guid?>(
                            networkProfile,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromNetworkProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.NetworkProfiles
            .Where(networkProfile =>
                request.ChildIds.Contains(networkProfile.Id) &&
                EF.Property<Guid?>(
                    networkProfile,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    networkProfile =>
                        EF.Property<Guid?>(
                            networkProfile,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
