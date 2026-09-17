using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class DeviceGroupEndpoints
{
    public static IEndpointRouteBuilder MapDeviceGroupEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceGroup").WithTags("DeviceGroups");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToDevices);
    group.MapDelete("/", removeFromDevices);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceGroupRequest request,
        IDeviceGroupService service,
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
        DeviceGroupRequest request,
        IDeviceGroupService service,
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
        IDeviceGroupService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DeviceGroupResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {

        var deviceGroup = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return deviceGroup is null ? Results.NotFound() : Results.Ok( deviceGroup );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IDeviceGroupService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDevicesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDevicesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@79cfde2b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@79cfde2b( com.harbormaster.codetemplate.model.classes.ClassObject@79cfde2bRequest request ) {
        var model = new DeviceGroup
        {
            Id = request.id,
        Name = request.Name
        Criteria = request.Criteria
        Tenant = request.Tenant
        Devices = request.Devices
        }
        return model;
    }
}
