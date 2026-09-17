using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class NetworkProfileEndpoints
{
    public static IEndpointRouteBuilder MapNetworkProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/networkProfile").WithTags("NetworkProfiles");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        INetworkProfileService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        INetworkProfileService service,
        CancellationToken cancellationToken)
    {
        var networkProfile = await service.GetByIdAsync(id, cancellationToken);
        return networkProfile is null ? Results.NotFound() : Results.Ok(ToResponse( networkProfile ));
    }

    private static async Task<IResult> Create(
        CreateNetworkProfileRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken)
    {
        var networkProfile = new NetworkProfile
        {
            Id = Guid.NewGuid(),

                ProfileName = request.ProfileName,
                Ssid = request.Ssid,
                Apn = request.Apn,
                ConnectivityType = request.ConnectivityType,

                IoTDeviceId = request.IoTDeviceId,
                GatewayId = request.GatewayId,
                SimCardId = request.SimCardId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/networkProfiles/networkProfile.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateNetworkProfileRequest request,
        INetworkProfileService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new NetworkProfile
        {
            Id = id,
            ProfileName = request.ProfileName,
            Ssid = request.Ssid,
            Apn = request.Apn,
            ConnectivityType = request.ConnectivityType,

            IoTDeviceId = request.IoTDeviceId,
            GatewayId = request.GatewayId,
            SimCardId = request.SimCardId,
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
        INetworkProfileService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static NetworkProfileResponse ToResponse(NetworkProfile lowercaseClassName)
        => new( networkProfile.Id,
                , String, String, String, ConnectivityType
                , IoTDeviceId, GatewayId, SimCardId );

}
