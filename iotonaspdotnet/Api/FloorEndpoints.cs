using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class FloorEndpoints
{
    public static IEndpointRouteBuilder MapFloorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/floor").WithTags("Floors");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IFloorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IFloorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateFloorRequest request,
        IFloorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Floor
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                Level = request.Level,

                BuildingId = request.BuildingId

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
        UpdateFloorRequest request,
        IFloorService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Floor
        {
            Id = id,
            FloorNumber = request.FloorNumber,
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
        IFloorService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static FloorResponse ToResponse(Floor lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.FloorNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
