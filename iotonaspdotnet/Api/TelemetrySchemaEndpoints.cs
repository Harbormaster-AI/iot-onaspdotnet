using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TelemetrySchemaEndpoints
{
    public static IEndpointRouteBuilder MapTelemetrySchemaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/telemetrySchema").WithTags("TelemetrySchemas");

        group.MapPost("/", Create);
        group.MapGet("/", Get);
        group.MapGet("/", GetAll);
        group.MapPut("/", Update);
        group.MapDelete("/", Delete);


    group.MapPut("/", AddToStreams);
    group.MapPut("/", RemoveFromStreams);


        return app;
    }

    private static async Task<IResult> Create(
        TelemetrySchemaRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTelemetrySchema( request );

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
        TelemetrySchemaRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTelemetrySchema( request );

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
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var telemetrySchema = await service.Get(identifier, cancellationToken);
        return telemetrySchema is null ? Results.NotFound() : Results.Ok( telemetrySchema );
    }


    private static async Task<IResult> GetAll(
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TelemetrySchemaResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToStreams(
        MultipleAssociationRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToStreams(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromStreams(
        MultipleAssociationRequest request,
        ITelemetrySchemaService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromStreams(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TelemetrySchema mapRequestToTelemetrySchema( TelemetrySchemaRequest request ) {
        var model = new TelemetrySchema
        {
            Id = request.Id,
            SchemaId = request.SchemaId,
            SchemaUri = request.SchemaUri,
            Encoding = request.Encoding,
        };
        return model;
    }

}
