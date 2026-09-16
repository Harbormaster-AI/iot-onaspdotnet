using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class ProvisioningRecordEndpoints
{
    public static IEndpointRouteBuilder MapProvisioningRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/provisioningRecord").WithTags("ProvisioningRecords");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IProvisioningRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IProvisioningRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateProvisioningRecordRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ProvisioningRecord
        {
            Id = Guid.NewGuid(),

                EnrolledAt = request.EnrolledAt,
                ProvisioningService = request.ProvisioningService,
                Method = request.Method,
                Status = request.Status,

                IoTDeviceId = request.IoTDeviceId
                DeviceCertificateId = request.DeviceCertificateId
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
        UpdateProvisioningRecordRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ProvisioningRecord
        {
            Id = id,
            ProvisioningRecordNumber = request.ProvisioningRecordNumber,
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
        IProvisioningRecordService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static ProvisioningRecordResponse ToResponse(ProvisioningRecord lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.ProvisioningRecordNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
