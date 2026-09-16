using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class SimCardEndpoints
{
    public static IEndpointRouteBuilder MapSimCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/simCard").WithTags("SimCards");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ISimCardService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISimCardService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateSimCardRequest request,
        ISimCardService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SimCard
        {
            Id = Guid.NewGuid(),

                Iccid = request.Iccid,
                Imsi = request.Imsi,
                Carrier = request.Carrier,
                Status = request.Status,

                TenantId = request.TenantId
                ConnectivityPlanId = request.ConnectivityPlanId

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
        UpdateSimCardRequest request,
        ISimCardService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SimCard
        {
            Id = id,
            SimCardNumber = request.SimCardNumber,
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
        ISimCardService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static SimCardResponse ToResponse(SimCard lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.SimCardNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
