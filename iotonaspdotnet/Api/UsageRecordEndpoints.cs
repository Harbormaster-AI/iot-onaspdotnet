using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class UsageRecordEndpoints
{
    public static IEndpointRouteBuilder MapUsageRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usageRecord").WithTags("UsageRecords");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IUsageRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IUsageRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateUsageRecordRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new UsageRecord
        {
            Id = Guid.NewGuid(),

                PeriodStart = request.PeriodStart,
                PeriodEnd = request.PeriodEnd,
                MessagesSent = request.MessagesSent,
                DataVolumeMB = request.DataVolumeMB,

                TenantId = request.TenantId,
                IoTDeviceId = request.IoTDeviceId,
                ConnectivityPlanId = request.ConnectivityPlanId,

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
        UpdateUsageRecordRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new UsageRecord
        {
            Id = id,
            UsageRecordNumber = request.UsageRecordNumber,
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
        IUsageRecordService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static UsageRecordResponse ToResponse(UsageRecord lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.UsageRecordNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
