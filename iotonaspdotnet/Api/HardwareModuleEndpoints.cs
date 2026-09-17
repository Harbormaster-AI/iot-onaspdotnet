using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class HardwareModuleEndpoints
{
    public static IEndpointRouteBuilder MapHardwareModuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hardwareModule").WithTags("HardwareModules");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignVendor);
        group.MapDelete("/", unassignVendor);


        return app;
    }

    private static async Task<IResult> Create(
        HardwareModuleRequest request,
        IHardwareModuleService service,
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
        HardwareModuleRequest request,
        IHardwareModuleService service,
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
        IHardwareModuleService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( HardwareModuleResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IHardwareModuleService service,
        CancellationToken cancellationToken) {

        var hardwareModule = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return hardwareModule is null ? Results.NotFound() : Results.Ok( hardwareModule );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IHardwareModuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
        AssociationRequest request,
        IHardwareModuleService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignVendorAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
    AssociationRequest request,
    IHardwareModuleService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignVendorAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
