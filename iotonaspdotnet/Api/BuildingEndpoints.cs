using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class BuildingEndpoints
{
    public static IEndpointRouteBuilder MapBuildingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/building").WithTags("Buildings");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IBuildingService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IBuildingService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateBuildingRequest request,
        IBuildingService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Building
        {
            Id = Guid.NewGuid(),

                Name = request.Name,

                SiteId = request.SiteId,

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
        UpdateBuildingRequest request,
        IBuildingService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Building
        {
            Id = id,
            BuildingNumber = request.BuildingNumber,
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
        IBuildingService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static BuildingResponse ToResponse(Building lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.BuildingNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
