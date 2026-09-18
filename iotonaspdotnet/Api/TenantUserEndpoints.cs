using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class TenantUserEndpoints
{
    public static IEndpointRouteBuilder MapTenantUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tenantUser").WithTags("TenantUsers");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);

    group.MapDelete("/", addToCommandInvocations);
    group.MapDelete("/", removeFromCommandInvocations);


        return app;
    }

    private static async Task<IResult> Create(
        TenantUserRequest request,
        ITenantUserService service,
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
        TenantUserRequest request,
        ITenantUserService service,
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
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TenantUserResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var tenantUser = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return tenantUser is null ? Results.NotFound() : Results.Ok( tenantUser );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    ITenantUserService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToCommandInvocationsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCommandInvocations(
        AssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromCommandInvocationsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private TenantUser mapRequestToTenantUser( TenantUserRequest request ) {
        var model = new TenantUser
        {
            Id = request.id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Tenant = request.Tenant,
            CommandInvocations = request.CommandInvocations,
            Role = request.Role,
        }
        return model;
    }
}
