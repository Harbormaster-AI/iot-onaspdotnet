
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class AccessPolicyRepository : IAccessPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public AccessPolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AccessPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AccessPolicys
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AccessPolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AccessPolicys
            .AsNoTracking()
            .Include(x => x.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Add(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Update(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        _db.AccessPolicys.Remove(accessPolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToApiKeysAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ApiKeys
            .Where(apiKey =>
                request.ChildIds.Contains(apiKey.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    apiKey =>
                        EF.Property<Guid?>(
                            apiKey,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApiKeysAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ApiKeys
            .Where(apiKey =>
                request.ChildIds.Contains(apiKey.Id) &&
                EF.Property<Guid?>(
                    apiKey,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    apiKey =>
                        EF.Property<Guid?>(
                            apiKey,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TenantUsers
            .Where(tenantUser =>
                request.ChildIds.Contains(tenantUser.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tenantUser =>
                        EF.Property<Guid?>(
                            tenantUser,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromUsersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TenantUsers
            .Where(tenantUser =>
                request.ChildIds.Contains(tenantUser.Id) &&
                EF.Property<Guid?>(
                    tenantUser,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    tenantUser =>
                        EF.Property<Guid?>(
                            tenantUser,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
