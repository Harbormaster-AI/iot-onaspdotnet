using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TwinTemplateEndpoints
{
    public static IEndpointRouteBuilder MapTwinTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/twinTemplate").WithTags("TwinTemplates");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);


    group.MapPut("/", AddToDeviceModels);
    group.MapPut("/", RemoveFromDeviceModels);


        return app;
    }

    private static async Task<IResult> Create(
        TwinTemplateRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTwinTemplate( request );

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
        TwinTemplateRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTwinTemplate( request );

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
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var twinTemplate = await service.Get(identifier, cancellationToken);
        return twinTemplate is null ? Results.NotFound() : Results.Ok( twinTemplate );
    }


    private static async Task<IResult> GetAll(
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TwinTemplateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDeviceModels(
        MultipleAssociationRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeviceModels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeviceModels(
        MultipleAssociationRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeviceModels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TwinTemplate mapRequestToTwinTemplate( TwinTemplateRequest request ) {
        var model = new TwinTemplate
        {
            Id = request.Id,
            Name = request.Name,
            SchemaUri = request.SchemaUri,
            Version = request.Version,
        };
        return model;
    }

}
