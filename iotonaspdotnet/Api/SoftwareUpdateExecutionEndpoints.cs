using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

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

        group.MapPut("/", assignCampaign);
        group.MapPut("/", unassignCampaign);
        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);


        return app;
    }

    private static async Task<IResult> Create(
        SoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
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
        SoftwareUpdateExecutionRequest request,
        ISoftwareUpdateExecutionService service,
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
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {

        var softwareUpdateExecution = await service.Get(identifier, cancellationToken);
        return softwareUpdateExecution is null ? Results.NotFound() : Results.Ok( softwareUpdateExecution );
    }


    private static async Task<IResult> GetAll(
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SoftwareUpdateExecutionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCampaign(
        AssociationRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCampaign(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCampaign(
    AssociationRequest request,
    ISoftwareUpdateExecutionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCampaign(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        ISoftwareUpdateExecutionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    ISoftwareUpdateExecutionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SoftwareUpdateExecution mapRequestToSoftwareUpdateExecution( SoftwareUpdateExecutionRequest request ) {
        var model = new SoftwareUpdateExecution
        {
            Id = request.id,
            StartedAt = request.StartedAt,
            CompletedAt = request.CompletedAt,
            Campaign = request.Campaign,
            Device = request.Device,
            Status = request.Status,
        };
        return model;
    }

}
