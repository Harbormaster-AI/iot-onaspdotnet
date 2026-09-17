using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class TelemetrySchemaEndpoints
{
    public static IEndpointRouteBuilder MapTelemetrySchemaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetrySchema").WithTags("TelemetrySchemas");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);


    group.MapDelete("/", addToStreams);
    group.MapDelete("/", removeFromStreams);


        return app;
    }

    private static async Task<IResult> Create(
        TelemetrySchemaRequest request,
        ITelemetrySchemaService service,
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
        TelemetrySchemaRequest request,
        ITelemetrySchemaService service,
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
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TelemetrySchemaResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var telemetrySchema = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return telemetrySchema is null ? Results.NotFound() : Results.Ok( telemetrySchema );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToStreamsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignStreams(
        AssociationRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromStreamsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private com.harbormaster.codetemplate.model.classes.ClassObject@647a7e40 mapRequestTocom.harbormaster.codetemplate.model.classes.ClassObject@647a7e40( com.harbormaster.codetemplate.model.classes.ClassObject@647a7e40Request request ) {
        var model = new TelemetrySchema
        {
            Id = request.id,
        SchemaId = request.SchemaId
        SchemaUri = request.SchemaUri
        Streams = request.Streams
        Encoding = request.Encoding
        }
        return model;
    }
}
