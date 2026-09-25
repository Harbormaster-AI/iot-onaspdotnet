
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TenantUserRepository : ITenantUserRepository
{
    private readonly ApplicationDbContext _db;

    public TenantUserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TenantUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TenantUsers
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TenantUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TenantUsers
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TenantUser tenantUser, CancellationToken cancellationToken)
    {
        _db.TenantUsers.Add(tenantUser);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TenantUser tenantUser, CancellationToken cancellationToken)
    {
        _db.TenantUsers.Update(tenantUser);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TenantUser tenantUser, CancellationToken cancellationToken)
    {
        _db.TenantUsers.Remove(tenantUser);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id) &&
                EF.Property<Guid?>(
                    commandInvocation,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
