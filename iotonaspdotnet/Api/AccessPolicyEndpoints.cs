using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class AccessPolicyEndpoints
{
    public static IEndpointRouteBuilder MapAccessPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accessPolicy").WithTags("AccessPolicys");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);

    group.MapPut("/", addToApiKeys);
    group.MapPut("/", removeFromApiKeys);

    group.MapPut("/", addToUsers);
    group.MapPut("/", removeFromUsers);


        return app;
    }

    private static async Task<IResult> Create(
        AccessPolicyRequest request,
        IAccessPolicyService service,
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
        AccessPolicyRequest request,
        IAccessPolicyService service,
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
        IAccessPolicyService service,
        CancellationToken cancellationToken) {

        var accessPolicy = await service.Get(identifier, cancellationToken);
        return accessPolicy is null ? Results.NotFound() : Results.Ok( accessPolicy );
    }


    private static async Task<IResult> GetAll(
        IAccessPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AccessPolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IAccessPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToApiKeys(
        MultipleAssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApiKeys(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApiKeys(
        MultipleAssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApiKeys(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToUsers(
        MultipleAssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUsers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUsers(
        MultipleAssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUsers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private AccessPolicy mapRequestToAccessPolicy( AccessPolicyRequest request ) {
        var model = new AccessPolicy
        {
            Id = request.id,
            Name = request.Name,
            Scope = request.Scope,
            ExpiresAt = request.ExpiresAt,
            Tenant = request.Tenant,
            ApiKeys = request.ApiKeys,
            Users = request.Users,
        }
        return model;
    }

}
