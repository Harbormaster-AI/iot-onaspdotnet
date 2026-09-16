using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class TenantUserEndpoints
{
    public static IEndpointRouteBuilder MapTenantUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tenantUser").WithTags("TenantUsers");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ITenantUserService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ITenantUserService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateTenantUserRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TenantUser
        {
            Id = Guid.NewGuid(),

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role,

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

        return Results.Created($"/api/lowercaseClassNames/{lowercaseClassName.Id}", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateTenantUserRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TenantUser
        {
            Id = id,
            TenantUserNumber = request.TenantUserNumber,
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
        ITenantUserService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static TenantUserResponse ToResponse(TenantUser lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.TenantUserNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
