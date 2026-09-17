using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignFirmwareRelease);
        group.MapDelete("/", unassignFirmwareRelease);
        group.MapDelete("/", assignDeviceGroup);
        group.MapDelete("/", unassignDeviceGroup);

    group.MapDelete("/", addToExecutions);
    group.MapDelete("/", removeFromExecutions);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareUpdateCampaignRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
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
            var updated = await service.UpdateAsync(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetAll(
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( SoftwareUpdateCampaignResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {

        var softwareUpdateCampaign = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return softwareUpdateCampaign is null ? Results.NotFound() : Results.Ok( softwareUpdateCampaign );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFirmwareRelease(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignFirmwareReleaseAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFirmwareRelease(
    AssociationRequest request,
    ISoftwareUpdateCampaignService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignFirmwareReleaseAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceGroup(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceGroupAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceGroup(
    AssociationRequest request,
    ISoftwareUpdateCampaignService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceGroupAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignExecutions(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToExecutionsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignExecutions(
        AssociationRequest request,
        ISoftwareUpdateCampaignService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromExecutionsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@628e09f4 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@628e09f4( com.harbormaster.codetemplate.model.classes.ClassObject@628e09f4Request request ) {
        var model = new SoftwareUpdateCampaign
        {
            Id = request.id,
        CampaignCode = request.CampaignCode
        ScheduledStart = request.ScheduledStart
        ScheduledEnd = request.ScheduledEnd
        FirmwareRelease = request.FirmwareRelease
        DeviceGroup = request.DeviceGroup
        Executions = request.Executions
        Status = request.Status
        }
        return model;
    }
}
