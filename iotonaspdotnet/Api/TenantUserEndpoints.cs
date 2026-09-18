using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TenantUserEndpoints
{
    public static IEndpointRouteBuilder MapTenantUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tenantUser").WithTags("TenantUsers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/", AssignTenant);
        group.MapPut("/", UnassignTenant);

    group.MapPut("/", AddToCommandInvocations);
    group.MapPut("/", RemoveFromCommandInvocations);


        return app;
    }

    private static async Task<IResult> Create(
        TenantUserRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTenantUser( request );

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
        TenantUserRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTenantUser( request );

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
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var tenantUser = await service.Get(identifier, cancellationToken);
        return tenantUser is null ? Results.NotFound() : Results.Ok( tenantUser );
    }


    private static async Task<IResult> GetAll(
        ITenantUserService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TenantUserResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    ITenantUserService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCommandInvocations(
        MultipleAssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCommandInvocations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCommandInvocations(
        MultipleAssociationRequest request,
        ITenantUserService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCommandInvocations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TenantUser mapRequestToTenantUser( TenantUserRequest request ) {
        var model = new TenantUser
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Role = request.Role,
        };
        return model;
    }

}
