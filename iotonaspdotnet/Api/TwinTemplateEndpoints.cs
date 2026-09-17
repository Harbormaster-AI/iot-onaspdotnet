using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class TwinTemplateEndpoints
{
    public static IEndpointRouteBuilder MapTwinTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/twinTemplate").WithTags("TwinTemplates");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);


    group.MapDelete("/", addToDeviceModels);
    group.MapDelete("/", removeFromDeviceModels);


        return app;
    }

    private static async Task<IResult> Create(
        TwinTemplateRequest request,
        ITwinTemplateService service,
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
        TwinTemplateRequest request,
        ITwinTemplateService service,
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
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TwinTemplateResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {

        var twinTemplate = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return twinTemplate is null ? Results.NotFound() : Results.Ok( twinTemplate );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignDeviceModels(
        AssociationRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDeviceModelsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceModels(
        AssociationRequest request,
        ITwinTemplateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDeviceModelsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@30ae56f4 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@30ae56f4( com.harbormaster.codetemplate.model.classes.ClassObject@30ae56f4Request request ) {
        var model = new TwinTemplate
        {
            Id = request.id,
        Name = request.Name
        SchemaUri = request.SchemaUri
        Version = request.Version
        DeviceModels = request.DeviceModels
        }
        return model;
    }
}
