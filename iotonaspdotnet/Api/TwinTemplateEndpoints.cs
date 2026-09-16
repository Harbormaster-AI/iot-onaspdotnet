using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class TwinTemplateEndpoints
{
    public static IEndpointRouteBuilder MapTwinTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/twinTemplate").WithTags("TwinTemplates");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ITwinTemplateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ITwinTemplateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateTwinTemplateRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TwinTemplate
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                SchemaUri = request.SchemaUri,
                Version = request.Version,


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
        UpdateTwinTemplateRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new TwinTemplate
        {
            Id = id,
            TwinTemplateNumber = request.TwinTemplateNumber,
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
        ITwinTemplateService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static TwinTemplateResponse ToResponse(TwinTemplate lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.TwinTemplateNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
