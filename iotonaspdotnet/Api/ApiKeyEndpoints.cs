using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class ApiKeyEndpoints
{
    public static IEndpointRouteBuilder MapApiKeyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/apiKey").WithTags("ApiKeys");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignAccessPolicy);
        group.MapDelete("/", unassignAccessPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        ApiKeyRequest request,
        IApiKeyService service,
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
        ApiKeyRequest request,
        IApiKeyService service,
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
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( ApiKeyResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var apiKey = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return apiKey is null ? Results.NotFound() : Results.Ok( apiKey );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IApiKeyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccessPolicy(
        AssociationRequest request,
        IApiKeyService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignAccessPolicyAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccessPolicy(
    AssociationRequest request,
    IApiKeyService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignAccessPolicyAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
