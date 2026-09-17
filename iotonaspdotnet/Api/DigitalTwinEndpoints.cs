using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignGateway);
        group.MapDelete("/", unassignGateway);
        group.MapDelete("/", assignTemplate);
        group.MapDelete("/", unassignTemplate);

    group.MapDelete("/", addToChangeEvents);
    group.MapDelete("/", removeFromChangeEvents);


        return app;
    }

    private static async Task<IResult> Create(
        DigitalTwinRequest request,
        IDigitalTwinService service,
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
        DigitalTwinRequest request,
        IDigitalTwinService service,
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
        IDigitalTwinService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DigitalTwinResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {

        var digitalTwin = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return digitalTwin is null ? Results.NotFound() : Results.Ok( digitalTwin );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTemplate(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTemplateAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTemplate(
    AssociationRequest request,
    IDigitalTwinService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTemplateAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignChangeEvents(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToChangeEventsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChangeEvents(
        AssociationRequest request,
        IDigitalTwinService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromChangeEventsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@78334954 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@78334954( com.harbormaster.codetemplate.model.classes.ClassObject@78334954Request request ) {
        var model = new DigitalTwin
        {
            Id = request.id,
        TwinId = request.TwinId
        DesiredStateVersion = request.DesiredStateVersion
        ReportedStateVersion = request.ReportedStateVersion
        LastSyncAt = request.LastSyncAt
        Device = request.Device
        Gateway = request.Gateway
        Template = request.Template
        ChangeEvents = request.ChangeEvents
        }
        return model;
    }
}
