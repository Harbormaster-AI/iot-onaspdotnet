using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class SoftwareUpdateCampaignEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareUpdateCampaignEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareUpdateCampaign").WithTags("SoftwareUpdateCampaigns");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignFirmwareRelease);
        group.MapPut("/", unassignFirmwareRelease);
        group.MapPut("/", assignDeviceGroup);
        group.MapPut("/", unassignDeviceGroup);

    group.MapPut("/", addToExecutions);
    group.MapPut("/", removeFromExecutions);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareUpdateCampaignRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        SoftwareUpdateCampaignRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var softwareUpdateCampaign = await service.Get(identifier, cancellationToken);
        return softwareUpdateCampaign is null ? Results.NotFound() : Results.Ok( softwareUpdateCampaign );
    }


    private static async Task<IResult> GetAll(
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SoftwareUpdateCampaignResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFirmwareRelease(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFirmwareRelease(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFirmwareRelease(
    AssociationRequest request,
    ISoftwareUpdateCampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFirmwareRelease(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceGroup(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeviceGroup(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeviceGroup(
    AssociationRequest request,
    ISoftwareUpdateCampaignService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeviceGroup(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToExecutions(
        MultipleAssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToExecutions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromExecutions(
        MultipleAssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromExecutions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SoftwareUpdateCampaign mapRequestToSoftwareUpdateCampaign( SoftwareUpdateCampaignRequest request ) {
        var model = new SoftwareUpdateCampaign
        {
            Id = request.Id,
            CampaignCode = request.CampaignCode,
            ScheduledStart = request.ScheduledStart,
            ScheduledEnd = request.ScheduledEnd,
            Status = request.Status,
        };
        return model;
    }

}
