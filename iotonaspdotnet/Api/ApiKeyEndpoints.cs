
using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class ApiKeyEndpoints
{
    public static IEndpointRouteBuilder MapApiKeyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/apiKey").WithTags("ApiKeys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAccessPolicy", AssignAccessPolicy);
        group.MapPut("/unassignAccessPolicy", UnassignAccessPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        ApiKeyRequest request,
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApiKey( request );

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
        ApiKeyRequest request,
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApiKey( request );

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
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var apiKey = await service.Get(identifier, cancellationToken);
        return apiKey is null ? Results.NotFound() : Results.Ok( apiKey );
    }


    private static async Task<IResult> GetAll(
        IApiKeyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ApiKeyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IApiKeyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccessPolicy(
        AssociationRequest request,
        IApiKeyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAccessPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAccessPolicy(
    AssociationRequest request,
    IApiKeyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAccessPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ApiKey mapRequestToApiKey( ApiKeyRequest request ) {
        var model = new ApiKey
        {
            Id = request.Id,
            KeyId = request.KeyId,
            HashedSecret = request.HashedSecret,
            CreatedAt = request.CreatedAt,
            LastUsedAt = request.LastUsedAt,
        };
        return model;
    }

}
