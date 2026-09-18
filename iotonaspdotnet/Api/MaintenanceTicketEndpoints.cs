using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class MaintenanceTicketEndpoints
{
    public static IEndpointRouteBuilder MapMaintenanceTicketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/maintenanceTicket").WithTags("MaintenanceTickets");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenanceTicketRequest request,
        IMaintenanceTicketService service,
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
        MaintenanceTicketRequest request,
        IMaintenanceTicketService service,
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
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {

        var maintenanceTicket = await service.Get(identifier, cancellationToken);
        return maintenanceTicket is null ? Results.NotFound() : Results.Ok( maintenanceTicket );
    }


    private static async Task<IResult> GetAll(
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MaintenanceTicketResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IMaintenanceTicketService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IMaintenanceTicketService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MaintenanceTicket mapRequestToMaintenanceTicket( MaintenanceTicketRequest request ) {
        var model = new MaintenanceTicket
        {
            Id = request.Id,
            TicketNumber = request.TicketNumber,
            OpenedAt = request.OpenedAt,
            ClosedAt = request.ClosedAt,
            Priority = request.Priority,
            Status = request.Status,
        };
        return model;
    }

}
