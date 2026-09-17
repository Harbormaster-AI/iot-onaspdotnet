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


    group.MapDelete("/", addToDeviceModels);
    group.MapDelete("/", removeFromDeviceModels);

    group.MapDelete("/", addToFirmwareReleases);
    group.MapDelete("/", removeFromFirmwareReleases);

    group.MapDelete("/", addToHardwareModules);
    group.MapDelete("/", removeFromHardwareModules);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceVendorRequest request,
        IDeviceVendorService service,
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
        DeviceVendorRequest request,
        IDeviceVendorService service,
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
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DeviceVendorResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {

        var deviceVendor = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return deviceVendor is null ? Results.NotFound() : Results.Ok( deviceVendor );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignDeviceModels(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDeviceModelsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModels(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDeviceModelsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400( com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400Request request ) {
        var model = new DeviceVendor
        {
            Id = request.id,
        Name = request.Name
        LegalName = request.LegalName
        HeadquartersCountry = request.HeadquartersCountry
        Website = request.Website
        DeviceModels = request.DeviceModels
        FirmwareReleases = request.FirmwareReleases
        HardwareModules = request.HardwareModules
        }
        return model;
    }
    private static async Task<IResult> AssignFirmwareReleases(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToFirmwareReleasesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFirmwareReleases(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromFirmwareReleasesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400( com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400Request request ) {
        var model = new DeviceVendor
        {
            Id = request.id,
        Name = request.Name
        LegalName = request.LegalName
        HeadquartersCountry = request.HeadquartersCountry
        Website = request.Website
        DeviceModels = request.DeviceModels
        FirmwareReleases = request.FirmwareReleases
        HardwareModules = request.HardwareModules
        }
        return model;
    }
    private static async Task<IResult> AssignHardwareModules(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToHardwareModulesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignHardwareModules(
        AssociationRequest request,
        IDeviceVendorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromHardwareModulesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400( com.harbormaster.codetemplate.model.classes.ClassObject@6a7bf400Request request ) {
        var model = new DeviceVendor
        {
            Id = request.id,
        Name = request.Name
        LegalName = request.LegalName
        HeadquartersCountry = request.HeadquartersCountry
        Website = request.Website
        DeviceModels = request.DeviceModels
        FirmwareReleases = request.FirmwareReleases
        HardwareModules = request.HardwareModules
        }
        return model;
    }
}
