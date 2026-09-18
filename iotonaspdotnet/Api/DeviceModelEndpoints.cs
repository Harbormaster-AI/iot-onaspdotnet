using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class DeviceModelEndpoints
{
    public static IEndpointRouteBuilder MapDeviceModelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceModel").WithTags("DeviceModels");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);

        group.MapPut("/", AssignVendor);
        group.MapPut("/", UnassignVendor);
        group.MapPut("/", AssignTwinTemplate);
        group.MapPut("/", UnassignTwinTemplate);

    group.MapPut("/", AddToHardwareModules);
    group.MapPut("/", RemoveFromHardwareModules);

    group.MapPut("/", AddToFirmwareReleases);
    group.MapPut("/", RemoveFromFirmwareReleases);

    group.MapPut("/", AddToCommandDefinitions);
    group.MapPut("/", RemoveFromCommandDefinitions);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceModelRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceModel( request );

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
        DeviceModelRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceModel( request );

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
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var deviceModel = await service.Get(identifier, cancellationToken);
        return deviceModel is null ? Results.NotFound() : Results.Ok( deviceModel );
    }


    private static async Task<IResult> GetAll(
        IDeviceModelService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DeviceModelResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVendor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVendor(
    AssociationRequest request,
    IDeviceModelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVendor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTwinTemplate(
        AssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTwinTemplate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTwinTemplate(
    AssociationRequest request,
    IDeviceModelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTwinTemplate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToHardwareModules(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToHardwareModules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromHardwareModules(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromHardwareModules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFirmwareReleases(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFirmwareReleases(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFirmwareReleases(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFirmwareReleases(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCommandDefinitions(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCommandDefinitions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCommandDefinitions(
        MultipleAssociationRequest request,
        IDeviceModelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCommandDefinitions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DeviceModel mapRequestToDeviceModel( DeviceModelRequest request ) {
        var model = new DeviceModel
        {
            Id = request.Id,
            Name = request.Name,
            ModelNumber = request.ModelNumber,
            HardwareRevision = request.HardwareRevision,
            SupportedConnectivity = request.SupportedConnectivity,
            DefaultTelemetryEncoding = request.DefaultTelemetryEncoding,
        };
        return model;
    }

}
