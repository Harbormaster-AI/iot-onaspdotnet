using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class MessagingEndpointEndpoints
{
    public static IEndpointRouteBuilder MapMessagingEndpointEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messagingEndpoint").WithTags("MessagingEndpoints");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToStreams);
    group.MapDelete("/", removeFromStreams);


        return app;
    }

    private static async Task<IResult> Create(
        MessagingEndpointRequest request,
        IMessagingEndpointService service,
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
        MessagingEndpointRequest request,
        IMessagingEndpointService service,
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
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( MessagingEndpointResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var messagingEndpoint = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return messagingEndpoint is null ? Results.NotFound() : Results.Ok( messagingEndpoint );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IMessagingEndpointService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@38e2043b mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@38e2043b( com.harbormaster.codetemplate.model.classes.ClassObject@38e2043bRequest request ) {
        var model = new MessagingEndpoint
        {
            Id = request.id,
        Host = request.Host
        Port = request.Port
        Secure = request.Secure
        Tenant = request.Tenant
        Streams = request.Streams
        Protocol = request.Protocol
        }
        return model;
    }
}
