using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class DeviceVendorEndpoints
{
    public static IEndpointRouteBuilder MapDeviceVendorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceVendor").WithTags("DeviceVendors");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToDeviceModels", AddToDeviceModels);
    group.MapPut("/removeFromDeviceModels", RemoveFromDeviceModels);

    group.MapPut("/addToFirmwareReleases", AddToFirmwareReleases);
    group.MapPut("/removeFromFirmwareReleases", RemoveFromFirmwareReleases);

    group.MapPut("/addToHardwareModules", AddToHardwareModules);
    group.MapPut("/removeFromHardwareModules", RemoveFromHardwareModules);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceVendorRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceVendor( request );

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
        DeviceVendorRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceVendor( request );

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
    private static DeviceVendor mapRequestToDeviceVendor( DeviceVendorRequest request ) {
        var model = new DeviceVendor
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
        return model;
    }

}
