
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class AlertRuleRepository : IAlertRuleRepository
{
    private readonly ApplicationDbContext _db;

    public AlertRuleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AlertRules
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AlertRules
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Add(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Update(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        _db.AlertRules.Remove(alertRule);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id) &&
                EF.Property<Guid?>(
                    telemetryStream,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Alerts
            .Where(alert =>
                request.ChildIds.Contains(alert.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alert =>
                        EF.Property<Guid?>(
                            alert,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Alerts
            .Where(alert =>
                request.ChildIds.Contains(alert.Id) &&
                EF.Property<Guid?>(
                    alert,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alert =>
                        EF.Property<Guid?>(
                            alert,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
