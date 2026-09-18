using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class DataRetentionPolicyEndpoints
{
    public static IEndpointRouteBuilder MapDataRetentionPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataRetentionPolicy").WithTags("DataRetentionPolicys");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);

        group.MapPut("/", AssignTenant);
        group.MapPut("/", UnassignTenant);

    group.MapPut("/", AddToStreams);
    group.MapPut("/", RemoveFromStreams);


        return app;
    }

    private static async Task<IResult> Create(
        DataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
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
        DataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
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
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {

        var dataRetentionPolicy = await service.Get(identifier, cancellationToken);
        return dataRetentionPolicy is null ? Results.NotFound() : Results.Ok( dataRetentionPolicy );
    }


    private static async Task<IResult> GetAll(
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DataRetentionPolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IDataRetentionPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToStreams(
        MultipleAssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromStreams(
        MultipleAssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static DataRetentionPolicy mapRequestToDataRetentionPolicy( DataRetentionPolicyRequest request ) {
        var model = new DataRetentionPolicy
        {
            Id = request.Id,
            Name = request.Name,
            RetentionDays = request.RetentionDays,
        };
        return model;
    }

}
