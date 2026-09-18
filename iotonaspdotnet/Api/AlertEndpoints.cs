using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class AlertEndpoints
{
    public static IEndpointRouteBuilder MapAlertEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/alert").WithTags("Alerts");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);

        group.MapPut("/", AssignDevice);
        group.MapPut("/", UnassignDevice);
        group.MapPut("/", AssignAlertRule);
        group.MapPut("/", UnassignAlertRule);


        return app;
    }

    private static async Task<IResult> Create(
        AlertRequest request,
        IAlertService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAlert( request );

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
        AlertRequest request,
        IAlertService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAlert( request );

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
        IAlertService service,
        CancellationToken cancellationToken) {

        var alert = await service.Get(identifier, cancellationToken);
        return alert is null ? Results.NotFound() : Results.Ok( alert );
    }


    private static async Task<IResult> GetAll(
        IAlertService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AlertResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAlertService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IAlertService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IAlertService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAlertRule(
        AssociationRequest request,
        IAlertService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAlertRule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAlertRule(
    AssociationRequest request,
    IAlertService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAlertRule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Alert mapRequestToAlert( AlertRequest request ) {
        var model = new Alert
        {
            Id = request.Id,
            RaisedAt = request.RaisedAt,
            ClearedAt = request.ClearedAt,
            Message = request.Message,
            Status = request.Status,
        };
        return model;
    }

}
