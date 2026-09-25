
using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class FirmwareReleaseEndpoints
{
    public static IEndpointRouteBuilder MapFirmwareReleaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/firmwareRelease").WithTags("FirmwareReleases");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDeviceModel", AssignDeviceModel);
        group.MapPut("/unassignDeviceModel", UnassignDeviceModel);


        return app;
    }

    private static async Task<IResult> Create(
        FirmwareReleaseRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFirmwareRelease( request );

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
        FirmwareReleaseRequest request,
        IFirmwareReleaseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFirmwareRelease( request );

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


    private static FirmwareRelease mapRequestToFirmwareRelease( FirmwareReleaseRequest request ) {
        var model = new FirmwareRelease
        {
            Id = request.Id,
            Version = request.Version,
            ReleaseDate = request.ReleaseDate,
            ReleaseNotes = request.ReleaseNotes,
            Checksum = request.Checksum,
        };
        return model;
    }

}
