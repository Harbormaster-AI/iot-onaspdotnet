using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class DeviceVendorEndpoints
{
    public static IEndpointRouteBuilder MapDeviceVendorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceVendor").WithTags("DeviceVendors");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);


    group.MapPut("/", addToDeviceModels);
    group.MapPut("/", removeFromDeviceModels);

    group.MapPut("/", addToFirmwareReleases);
    group.MapPut("/", removeFromFirmwareReleases);

    group.MapPut("/", addToHardwareModules);
    group.MapPut("/", removeFromHardwareModules);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceVendorRequest request,
        IDeviceVendorService service,
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
        DeviceVendorRequest request,
        IDeviceVendorService service,
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
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var deviceVendor = await service.Get(identifier, cancellationToken);
        return deviceVendor is null ? Results.NotFound() : Results.Ok( deviceVendor );
    }


    private static async Task<IResult> GetAll(
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DeviceVendorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDeviceModels(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeviceModels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeviceModels(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeviceModels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFirmwareReleases(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFirmwareReleases(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFirmwareReleases(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFirmwareReleases(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToHardwareModules(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToHardwareModules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromHardwareModules(
        MultipleAssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromHardwareModules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private DeviceVendor mapRequestToDeviceVendor( DeviceVendorRequest request ) {
        var model = new DeviceVendor
        {
            Id = request.id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
            DeviceModels = request.DeviceModels,
            FirmwareReleases = request.FirmwareReleases,
            HardwareModules = request.HardwareModules,
        };
        return model;
    }

}
