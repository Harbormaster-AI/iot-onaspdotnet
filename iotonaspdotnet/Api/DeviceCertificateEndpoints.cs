using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class DeviceCertificateEndpoints
{
    public static IEndpointRouteBuilder MapDeviceCertificateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/deviceCertificate").WithTags("DeviceCertificates");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IDeviceCertificateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IDeviceCertificateService service,
        CancellationToken cancellationToken)
    {
        var deviceCertificate = await service.GetByIdAsync(id, cancellationToken);
        return deviceCertificate is null ? Results.NotFound() : Results.Ok(ToResponse( deviceCertificate ));
    }

    private static async Task<IResult> Create(
        CreateDeviceCertificateRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken)
    {
        var deviceCertificate = new DeviceCertificate
        {
            Id = Guid.NewGuid(),

                SerialNumber = request.SerialNumber,
                NotBefore = request.NotBefore,
                NotAfter = request.NotAfter,
                Fingerprint = request.Fingerprint,
                CertificateType = request.CertificateType,

                IoTDeviceId = request.IoTDeviceId,
                GatewayId = request.GatewayId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/deviceCertificates/deviceCertificate.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateDeviceCertificateRequest request,
        IDeviceCertificateService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new DeviceCertificate
        {
            Id = id,
            SerialNumber = request.SerialNumber,
            NotBefore = request.NotBefore,
            NotAfter = request.NotAfter,
            Fingerprint = request.Fingerprint,
            CertificateType = request.CertificateType,

            IoTDeviceId = request.IoTDeviceId,
            GatewayId = request.GatewayId,
        };

        try
        {
            var updated = await service.UpdateAsync(lowercaseClassName, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> Delete(
        Guid id,
        IDeviceCertificateService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static DeviceCertificateResponse ToResponse(DeviceCertificate lowercaseClassName)
        => new( deviceCertificate.Id,
                , String, DateTime, DateTime, String, CertificateType
                , IoTDeviceId, GatewayId );

}
