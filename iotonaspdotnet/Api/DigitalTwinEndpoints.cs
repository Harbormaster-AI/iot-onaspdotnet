using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class DigitalTwinEndpoints
{
    public static IEndpointRouteBuilder MapDigitalTwinEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/digitalTwin").WithTags("DigitalTwins");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignGateway);
        group.MapPut("/", unassignGateway);
        group.MapPut("/", assignTemplate);
        group.MapPut("/", unassignTemplate);

    group.MapPut("/", addToChangeEvents);
    group.MapPut("/", removeFromChangeEvents);


        return app;
    }

    private static async Task<IResult> Create(
        DigitalTwinRequest request,
        IDigitalTwinService service,
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
        DigitalTwinRequest request,
        IDigitalTwinService service,
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
        IDigitalTwinService service,
        CancellationToken cancellationToken) {

        var digitalTwin = await service.Get(identifier, cancellationToken);
        return digitalTwin is null ? Results.NotFound() : Results.Ok( digitalTwin );
    }


    private static async Task<IResult> GetAll(
        IDigitalTwinService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DigitalTwinResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGateway(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGateway(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGateway(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTemplate(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTemplate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTemplate(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTemplate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChangeEvents(
        MultipleAssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChangeEvents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChangeEvents(
        MultipleAssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChangeEvents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DigitalTwin mapRequestToDigitalTwin( DigitalTwinRequest request ) {
        var model = new DigitalTwin
        {
            Id = request.id,
            TwinId = request.TwinId,
            DesiredStateVersion = request.DesiredStateVersion,
            ReportedStateVersion = request.ReportedStateVersion,
            LastSyncAt = request.LastSyncAt,
            Device = request.Device,
            Gateway = request.Gateway,
            Template = request.Template,
            ChangeEvents = request.ChangeEvents,
        };
        return model;
    }

}
