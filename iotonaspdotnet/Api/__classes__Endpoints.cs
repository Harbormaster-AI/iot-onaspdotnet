#set( $className = $classObject.getName() )
#set( $lowercaseClassName = $Utils.lowercaseFirstLetter( $className ) )
using ${appName}.Service;
using ${appName}.Domain;

namespace ${appName}.Api;

public static class ${className}Endpoints
{
    public static IEndpointRouteBuilder Map${className}Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/$lowercaseClassName").WithTags("${className}s");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        I${className}Service service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        I${className}Service service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        Create${className}Request request,
        I${className}Service service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ${className}
        {
            Id = Guid.NewGuid(),

#set( $includeHierarchy = false )
#set( $includePKs = false )
#set( $attributes = $classObject.getAttributesOnly( $includeHierarchy, $includePKs ) )
#foreach( $attribute in $attributes )
#set( $attributeName = $attribute.getName() )
#set( $attributeName = $Utils.capitalizeFirstLetter( $attributeName ) )
                ${attributeName} = request.${attributeName},
#end

#foreach( $singleAssociation = $singleAssociations )
#set( $type = $singleAssociation.getType() )
                ${type}Id = request.${type}Id
#end

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/lowercaseClassNames/{lowercaseClassName.Id}", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        Update${className}Request request,
        I${className}Service service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ${className}
        {
            Id = id,
            ${className}Number = request.${className}Number,
            Balance = request.Balance,
            CustomerId = request.CustomerId
        };

        try
        {
            var updated = await service.UpdateAsync(lowercaseClassName, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> Delete(
        Guid id,
        I${className}Service service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static ${className}Response ToResponse(${className} lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.${className}Number, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
