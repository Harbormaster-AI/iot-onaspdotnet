using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class SoftwareUpdateCampaignEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareUpdateCampaignEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareUpdateCampaign").WithTags("SoftwareUpdateCampaigns");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateSoftwareUpdateCampaignRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SoftwareUpdateCampaign
        {
            Id = Guid.NewGuid(),

                CampaignCode = request.CampaignCode,
                ScheduledStart = request.ScheduledStart,
                ScheduledEnd = request.ScheduledEnd,
                Status = request.Status,

                FirmwareReleaseId = request.FirmwareReleaseId,
                DeviceGroupId = request.DeviceGroupId,

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
        UpdateSoftwareUpdateCampaignRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new SoftwareUpdateCampaign
        {
            Id = id,
            SoftwareUpdateCampaignNumber = request.SoftwareUpdateCampaignNumber,
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
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static SoftwareUpdateCampaignResponse ToResponse(SoftwareUpdateCampaign lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.SoftwareUpdateCampaignNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
