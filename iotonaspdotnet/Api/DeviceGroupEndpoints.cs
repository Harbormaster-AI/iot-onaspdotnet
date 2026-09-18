using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);

    group.MapPut("/", addToDevices);
    group.MapPut("/", removeFromDevices);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceGroupRequest request,
        IDeviceGroupService service,
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
        DeviceGroupRequest request,
        IDeviceGroupService service,
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
        IDeviceGroupService service,
        CancellationToken cancellationToken) {

        var deviceGroup = await service.Get(identifier, cancellationToken);
        return deviceGroup is null ? Results.NotFound() : Results.Ok( deviceGroup );
    }


    private static async Task<IResult> GetAll(
        IDeviceGroupService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DeviceGroupResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IDeviceGroupService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        IDeviceGroupService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DeviceGroup mapRequestToDeviceGroup( DeviceGroupRequest request ) {
        var model = new DeviceGroup
        {
            Id = request.id,
            Name = request.Name,
            Criteria = request.Criteria,
        };
        return model;
    }

}
