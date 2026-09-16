#set( $className = $classObject.getName() )
#set( $lowercaseClassName = $Utils.lowercaseFirstLetter( $className ) )
#set( $singleAssociations = $classObject.getSingleAssociations() )
using ${appName}.Domain
using ${appName}.Persistence
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
using ${appName}.Persistence.${type}s;
#end

namespace ${appName}.Service

public interface I${className}Service
{
    Task<${className}?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<${className}>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class ${className}Service : I${className}Service
{
    private readonly I${className}Repository _repository;
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
    private readonly I${type}Repository _${lcType}s;
#end

    public ${className}Service(
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
        I${type}Repository ${lcType}s,
#end        
        I${className}Repository repository )
    {
        _repository = repository;
#foreach( $singleAssociation in $singleAssociations )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
        _${lcType}s = ${lcType}s;
#end
    }

    public Task<${className}?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<${className}>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken)
    {
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
        var ${lcType} = await _${lcType}s.GetByIdAsync(${lowercaseClassName}.${type}Id, cancellationToken)
            ?? throw new InvalidOperationException("${type} not found.");

        if (${lcType}.${className} is not null)
        {
            throw new InvalidOperationException("${type} already has a(n) ${lowercaseClassName} (1:1 relationship).");
        }

#end
        await _repository.AddAsync(${lowercaseClassName}, cancellationToken);
    }

    public async Task<bool> UpdateAsync(${className} ${lowercaseClassName}, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(${lowercaseClassName}.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 — do not reassign to a ${lcType} who already has another ${lowercaseClassName}.
        if (existing.${type}Id != ${lowercaseClassName}.${type}Id)
        {
            var target;
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
            target = await _${lcType}s.GetByIdAsync(${lowercaseClassName}.${type}Id, cancellationToken)
                ?? throw new InvalidOperationException("${type} not found.");

            if (target.${className} is not null && target.${className}.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ${lcType} already has an ${lowercaseClassName} (1:1 relationship).");
            }

        }
#set( $includeHierarchy = false )
#set( $includePKs = false )
#set( $attributes = $classObject.getAttributesOnly( $includeHierarchy, $includePKs ) )
#foreach( $attribute in $attributes )
#set( $attributeName = $attribute.getName() )
#set( $attributeName = $Utils.capitalizeFirstLetter( $attributeName ) )
        existing.attributeName = ${lowercaseClassName}.attributeName;
#end

#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
        existing.${type}Id = ${lowercaseClassName}.${type}Id;
#end
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
