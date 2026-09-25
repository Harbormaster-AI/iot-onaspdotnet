
using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class MessagingEndpointEndpoints
{
    public static IEndpointRouteBuilder MapMessagingEndpointEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messagingEndpoint").WithTags("MessagingEndpoints");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignTenant", AssignTenant);
        group.MapPut("/unassignTenant", UnassignTenant);

    group.MapPut("/addToStreams", AddToStreams);
    group.MapPut("/removeFromStreams", RemoveFromStreams);


        return app;
    }

    private static async Task<IResult> Create(
        MessagingEndpointRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMessagingEndpoint( request );

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
        MessagingEndpointRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMessagingEndpoint( request );

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
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var messagingEndpoint = await service.Get(identifier, cancellationToken);
        return messagingEndpoint is null ? Results.NotFound() : Results.Ok( messagingEndpoint );
    }


    private static async Task<IResult> GetAll(
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MessagingEndpointResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IMessagingEndpointService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToStreams(
        MultipleAssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromStreams(
        MultipleAssociationRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static MessagingEndpoint mapRequestToMessagingEndpoint( MessagingEndpointRequest request ) {
        var model = new MessagingEndpoint
        {
            Id = request.Id,
            Host = request.Host,
            Port = request.Port,
            Secure = request.Secure,
            Protocol = request.Protocol,
        };
        return model;
    }

}
