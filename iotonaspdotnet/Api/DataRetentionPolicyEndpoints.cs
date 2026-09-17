using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class DataRetentionPolicyEndpoints
{
    public static IEndpointRouteBuilder MapDataRetentionPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataRetentionPolicy").WithTags("DataRetentionPolicys");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken)
    {
        var dataRetentionPolicy = await service.GetByIdAsync(id, cancellationToken);
        return dataRetentionPolicy is null ? Results.NotFound() : Results.Ok(ToResponse( dataRetentionPolicy ));
    }

    private static async Task<IResult> Create(
        CreateDataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken)
    {
        var dataRetentionPolicy = new DataRetentionPolicy
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                RetentionDays = request.RetentionDays,

                TenantId = request.TenantId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/dataRetentionPolicys/dataRetentionPolicy.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateDataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DataRetentionPolicy
        {
            Id = id,
            Name = request.Name,
            RetentionDays = request.RetentionDays,

            TenantId = request.TenantId,
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
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static DataRetentionPolicyResponse ToResponse(DataRetentionPolicy lowercaseClassName)
        => new( dataRetentionPolicy.Id,
                , String, Integer
                , TenantId );

}
