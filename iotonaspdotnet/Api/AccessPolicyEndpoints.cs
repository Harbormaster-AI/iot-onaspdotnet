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

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToApiKeys);
    group.MapDelete("/", removeFromApiKeys);

    group.MapDelete("/", addToUsers);
    group.MapDelete("/", removeFromUsers);


        return app;
    }

    private static async Task<IResult> Create(
        AccessPolicyRequest request,
        IAccessPolicyService service,
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
        AccessPolicyRequest request,
        IAccessPolicyService service,
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
        IAccessPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( AccessPolicyResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {

        var accessPolicy = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return accessPolicy is null ? Results.NotFound() : Results.Ok( accessPolicy );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IAccessPolicyService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignApiKeys(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToApiKeysAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApiKeys(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromApiKeysAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private AccessPolicy mapRequestToAccessPolicy( AccessPolicyRequest request ) {
        var model = new AccessPolicy
        {
            Id = request.id,
            Name = request.Name;
            Scope = request.Scope;
            ExpiresAt = request.ExpiresAt;
            Tenant = request.Tenant;
            ApiKeys = request.ApiKeys;
            Users = request.Users;
        }
        return model;
    }
    private static async Task<IResult> AssignUsers(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToUsersAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUsers(
        AssociationRequest request,
        IAccessPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromUsersAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private AccessPolicy mapRequestToAccessPolicy( AccessPolicyRequest request ) {
        var model = new AccessPolicy
        {
            Id = request.id,
            Name = request.Name;
            Scope = request.Scope;
            ExpiresAt = request.ExpiresAt;
            Tenant = request.Tenant;
            ApiKeys = request.ApiKeys;
            Users = request.Users;
        }
        return model;
    }
}
