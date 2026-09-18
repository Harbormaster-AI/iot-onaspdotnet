using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class DeviceCertificateEndpoints
{
    public static IEndpointRouteBuilder MapDeviceCertificateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceCertificate").WithTags("DeviceCertificates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/", AssignDevice);
        group.MapPut("/", UnassignDevice);
        group.MapPut("/", AssignGateway);
        group.MapPut("/", UnassignGateway);


        return app;
    }

    private static async Task<IResult> Create(
        DeviceCertificateRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceCertificate( request );

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
        DeviceCertificateRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDeviceCertificate( request );

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
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var deviceCertificate = await service.Get(identifier, cancellationToken);
        return deviceCertificate is null ? Results.NotFound() : Results.Ok( deviceCertificate );
    }


    private static async Task<IResult> GetAll(
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DeviceCertificateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IDeviceCertificateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGateway(
        AssociationRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGateway(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGateway(
    AssociationRequest request,
    IDeviceCertificateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGateway(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static DeviceCertificate mapRequestToDeviceCertificate( DeviceCertificateRequest request ) {
        var model = new DeviceCertificate
        {
            Id = request.Id,
            SerialNumber = request.SerialNumber,
            NotBefore = request.NotBefore,
            NotAfter = request.NotAfter,
            Fingerprint = request.Fingerprint,
            CertificateType = request.CertificateType,
        };
        return model;
    }

}
