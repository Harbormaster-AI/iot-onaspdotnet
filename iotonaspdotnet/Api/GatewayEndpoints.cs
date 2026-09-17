using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class GatewayEndpoints
{
    public static IEndpointRouteBuilder MapGatewayEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/gateway").WithTags("Gateways");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IGatewayService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IGatewayService service,
        CancellationToken cancellationToken)
    {
        var gateway = await service.GetByIdAsync(id, cancellationToken);
        return gateway is null ? Results.NotFound() : Results.Ok(ToResponse( gateway ));
    }

    private static async Task<IResult> Create(
        CreateGatewayRequest request,
        IGatewayService service,
        CancellationToken cancellationToken)
    {
        var gateway = new Gateway
        {
            Id = Guid.NewGuid(),

                SoftwareVersion = request.SoftwareVersion,
                Status = request.Status,

                SiteId = request.SiteId,
                RoomId = request.RoomId,
                DigitalTwinId = request.DigitalTwinId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/gateways/gateway.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateGatewayRequest request,
        IGatewayService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new Gateway
        {
            Id = id,
            SoftwareVersion = request.SoftwareVersion,
            Status = request.Status,

            SiteId = request.SiteId,
            RoomId = request.RoomId,
            DigitalTwinId = request.DigitalTwinId,
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
        IGatewayService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static GatewayResponse ToResponse(Gateway lowercaseClassName)
        => new( gateway.Id,
                , String, DeviceStatus
                , SiteId, RoomId, DigitalTwinId );

}
