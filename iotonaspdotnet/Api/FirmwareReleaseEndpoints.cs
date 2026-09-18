using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class FirmwareReleaseEndpoints
{
    public static IEndpointRouteBuilder MapFirmwareReleaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/firmwareRelease").WithTags("FirmwareReleases");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDeviceModel);
        group.MapPut("/", unassignDeviceModel);


        return app;
    }

    private static async Task<IResult> Create(
        FirmwareReleaseRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        FirmwareReleaseRequest request,
        IFirmwareReleaseService service,
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
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {

        var firmwareRelease = await service.Get(identifier, cancellationToken);
        return firmwareRelease is null ? Results.NotFound() : Results.Ok( firmwareRelease );
    }


    private static async Task<IResult> GetAll(
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FirmwareReleaseResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModel(
        AssociationRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDeviceModel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDeviceModel(
    AssociationRequest request,
    IFirmwareReleaseService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDeviceModel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private FirmwareRelease mapRequestToFirmwareRelease( FirmwareReleaseRequest request ) {
        var model = new FirmwareRelease
        {
            Id = request.id,
            Version = request.Version,
            ReleaseDate = request.ReleaseDate,
            ReleaseNotes = request.ReleaseNotes,
            Checksum = request.Checksum,
            DeviceModel = request.DeviceModel,
        }
        return model;
    }

}
