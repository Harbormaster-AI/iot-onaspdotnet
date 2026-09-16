#set( $className = $classObject.getName() )
#set( $lowercaseClassName = $Utils.lowercaseFirstLetter( $className ) )
#set( $singleAssociations = $classObject.getSingleAssociations() )
using ${appName}.Domain;
using Microsoft.EntityFrameworkCore;

namespace ${appName}.Persistence;

public class ${className}Repository : I${className}Repository
{
    private readonly ApplicationDbContext _db;

    public ${className}Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<${className}?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.${className}s
#foreach( $singleAssociation = $singleAssociations )
#set( $type = $singleAssociation.getType() )
            .Include(x => x.${type})
#end
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<${className}>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.${className}s
            .AsNoTracking()
#foreach( $singleAssociation = $singleAssociations )
#set( $type = $singleAssociation.getType() )
            .Include(x => x.${type})
#end
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken)
    {
        _db.${className}s.Add(${lowercaseClassName});
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken)
    {
        _db.${className}s.Update(${lowercaseClassName});
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken)
    {
        _db.${className}s.Remove(${lowercaseClassName});
        await _db.SaveChangesAsync(cancellationToken);
    }
}
