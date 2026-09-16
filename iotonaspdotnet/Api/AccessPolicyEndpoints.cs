using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class AccessPolicyEndpoints
{
    public static IEndpointRouteBuilder MapAccessPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accessPolicy").WithTags("AccessPolicys");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IAccessPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IAccessPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateAccessPolicyRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new AccessPolicy
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                Scope = request.Scope,
                ExpiresAt = request.ExpiresAt,

                TenantId = request.TenantId

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
        UpdateAccessPolicyRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new AccessPolicy
        {
            Id = id,
            AccessPolicyNumber = request.AccessPolicyNumber,
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
        IAccessPolicyService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static AccessPolicyResponse ToResponse(AccessPolicy lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.AccessPolicyNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
