using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class SoftwareUpdateExecutionEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareUpdateExecutionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareUpdateExecution").WithTags("SoftwareUpdateExecutions");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateSoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SoftwareUpdateExecution
        {
            Id = Guid.NewGuid(),

                StartedAt = request.StartedAt,
                CompletedAt = request.CompletedAt,
                Status = request.Status,

                SoftwareUpdateCampaignId = request.SoftwareUpdateCampaignId
                IoTDeviceId = request.IoTDeviceId

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
        UpdateSoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SoftwareUpdateExecution
        {
            Id = id,
            SoftwareUpdateExecutionNumber = request.SoftwareUpdateExecutionNumber,
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
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static SoftwareUpdateExecutionResponse ToResponse(SoftwareUpdateExecution lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.SoftwareUpdateExecutionNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
