using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class DeviceCertificateEndpoints
{
    public static IEndpointRouteBuilder MapDeviceCertificateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceCertificate").WithTags("DeviceCertificates");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignGateway);
        group.MapDelete("/", unassignGateway);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceCertificateRequest request,
        IDeviceCertificateService service,
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
        DeviceCertificateRequest request,
        IDeviceCertificateService service,
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
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( DeviceCertificateResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var deviceCertificate = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return deviceCertificate is null ? Results.NotFound() : Results.Ok( deviceCertificate );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    IDeviceCertificateService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
    AssociationRequest request,
    IDeviceCertificateService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignGatewayAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
