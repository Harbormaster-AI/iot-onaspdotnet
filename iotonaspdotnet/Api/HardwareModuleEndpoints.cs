using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class HardwareModuleEndpoints
{
    public static IEndpointRouteBuilder MapHardwareModuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hardwareModule").WithTags("HardwareModules");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);

        group.MapPut("/", AssignVendor);
        group.MapPut("/", UnassignVendor);


        return app;
    }

    private static async Task<IResult> Create(
        HardwareModuleRequest request,
        IHardwareModuleService service,
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
        HardwareModuleRequest request,
        IHardwareModuleService service,
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
        IHardwareModuleService service,
        CancellationToken cancellationToken) {

        var hardwareModule = await service.Get(identifier, cancellationToken);
        return hardwareModule is null ? Results.NotFound() : Results.Ok( hardwareModule );
    }


    private static async Task<IResult> GetAll(
        IHardwareModuleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( HardwareModuleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IHardwareModuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVendor(
        AssociationRequest request,
        IHardwareModuleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVendor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVendor(
    AssociationRequest request,
    IHardwareModuleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVendor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static HardwareModule mapRequestToHardwareModule( HardwareModuleRequest request ) {
        var model = new HardwareModule
        {
            Id = request.Id,
            ModuleCode = request.ModuleCode,
            DatasheetUri = request.DatasheetUri,
            ModuleType = request.ModuleType,
        };
        return model;
    }

}
