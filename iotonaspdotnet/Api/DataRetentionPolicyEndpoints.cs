using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class DataRetentionPolicyEndpoints
{
    public static IEndpointRouteBuilder MapDataRetentionPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dataRetentionPolicy").WithTags("DataRetentionPolicys");

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
        DataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
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
        DataRetentionPolicyRequest request,
        IDataRetentionPolicyService service,
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
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DataRetentionPolicyResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {

        var dataRetentionPolicy = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return dataRetentionPolicy is null ? Results.NotFound() : Results.Ok( dataRetentionPolicy );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IDataRetentionPolicyService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        IDataRetentionPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private DataRetentionPolicy mapRequestToDataRetentionPolicy( DataRetentionPolicyRequest request ) {
        var model = new DataRetentionPolicy
        {
            Id = request.id,
            Name = request.Name;
            RetentionDays = request.RetentionDays;
            Tenant = request.Tenant;
            Streams = request.Streams;
        }
        return model;
    }
}
