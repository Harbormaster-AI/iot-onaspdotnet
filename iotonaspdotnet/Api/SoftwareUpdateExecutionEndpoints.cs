using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class SoftwareUpdateExecutionEndpoints
{
    public static IEndpointRouteBuilder MapSoftwareUpdateExecutionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/softwareUpdateExecution").WithTags("SoftwareUpdateExecutions");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignCampaign);
        group.MapDelete("/", unassignCampaign);
        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
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
        SoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
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
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( SoftwareUpdateExecutionResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {

        var softwareUpdateExecution = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return softwareUpdateExecution is null ? Results.NotFound() : Results.Ok( softwareUpdateExecution );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignCampaignAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
    AssociationRequest request,
    ISoftwareUpdateExecutionService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignCampaignAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    ISoftwareUpdateExecutionService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
