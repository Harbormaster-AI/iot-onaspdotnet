using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);


        return app;
    }

    private static async Task<IResult> Create(
        MaintenanceTicketRequest request,
        IMaintenanceTicketService service,
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
        MaintenanceTicketRequest request,
        IMaintenanceTicketService service,
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
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( MaintenanceTicketResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {

        var maintenanceTicket = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return maintenanceTicket is null ? Results.NotFound() : Results.Ok( maintenanceTicket );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    IMaintenanceTicketService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IMaintenanceTicketService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IMaintenanceTicketService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
