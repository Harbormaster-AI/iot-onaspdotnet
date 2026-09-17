using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class DeviceModelEndpoints
{
    public static IEndpointRouteBuilder MapDeviceModelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceModel").WithTags("DeviceModels");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignVendor);
        group.MapDelete("/", unassignVendor);
        group.MapDelete("/", assignTwinTemplate);
        group.MapDelete("/", unassignTwinTemplate);

    group.MapDelete("/", addToHardwareModules);
    group.MapDelete("/", removeFromHardwareModules);

    group.MapDelete("/", addToFirmwareReleases);
    group.MapDelete("/", removeFromFirmwareReleases);

    group.MapDelete("/", addToCommandDefinitions);
    group.MapDelete("/", removeFromCommandDefinitions);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceModelRequest request,
        IDeviceModelService service,
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
        DeviceModelRequest request,
        IDeviceModelService service,
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
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DeviceModelResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var deviceModel = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return deviceModel is null ? Results.NotFound() : Results.Ok( deviceModel );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignVendorAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
    AssociationRequest request,
    IDeviceModelService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignVendorAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwinTemplate(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTwinTemplateAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwinTemplate(
    AssociationRequest request,
    IDeviceModelService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTwinTemplateAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignHardwareModules(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToHardwareModulesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignHardwareModules(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromHardwareModulesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e( com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9eRequest request ) {
        var model = new DeviceModel
        {
            Id = request.id,
        Name = request.Name
        ModelNumber = request.ModelNumber
        HardwareRevision = request.HardwareRevision
        Vendor = request.Vendor
        HardwareModules = request.HardwareModules
        TwinTemplate = request.TwinTemplate
        FirmwareReleases = request.FirmwareReleases
        CommandDefinitions = request.CommandDefinitions
        SupportedConnectivity = request.SupportedConnectivity
        DefaultTelemetryEncoding = request.DefaultTelemetryEncoding
        }
        return model;
    }
    private static async Task<IResult> AssignFirmwareReleases(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToFirmwareReleasesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFirmwareReleases(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromFirmwareReleasesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e( com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9eRequest request ) {
        var model = new DeviceModel
        {
            Id = request.id,
        Name = request.Name
        ModelNumber = request.ModelNumber
        HardwareRevision = request.HardwareRevision
        Vendor = request.Vendor
        HardwareModules = request.HardwareModules
        TwinTemplate = request.TwinTemplate
        FirmwareReleases = request.FirmwareReleases
        CommandDefinitions = request.CommandDefinitions
        SupportedConnectivity = request.SupportedConnectivity
        DefaultTelemetryEncoding = request.DefaultTelemetryEncoding
        }
        return model;
    }
    private static async Task<IResult> AssignCommandDefinitions(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCommandDefinitionsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandDefinitions(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCommandDefinitionsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9e( com.harbormaster.codetemplate.model.classes.ClassObject@38b6ea9eRequest request ) {
        var model = new DeviceModel
        {
            Id = request.id,
        Name = request.Name
        ModelNumber = request.ModelNumber
        HardwareRevision = request.HardwareRevision
        Vendor = request.Vendor
        HardwareModules = request.HardwareModules
        TwinTemplate = request.TwinTemplate
        FirmwareReleases = request.FirmwareReleases
        CommandDefinitions = request.CommandDefinitions
        SupportedConnectivity = request.SupportedConnectivity
        DefaultTelemetryEncoding = request.DefaultTelemetryEncoding
        }
        return model;
    }
}
