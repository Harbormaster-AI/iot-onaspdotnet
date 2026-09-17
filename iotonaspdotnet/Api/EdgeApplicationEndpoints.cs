using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class EdgeApplicationEndpoints
{
    public static IEndpointRouteBuilder MapEdgeApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/edgeApplication").WithTags("EdgeApplications");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IEdgeApplicationService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IEdgeApplicationService service,
        CancellationToken cancellationToken)
    {
        var edgeApplication = await service.GetByIdAsync(id, cancellationToken);
        return edgeApplication is null ? Results.NotFound() : Results.Ok(ToResponse( edgeApplication ));
    }

    private static async Task<IResult> Create(
        CreateEdgeApplicationRequest request,
        IEdgeApplicationService service,
        CancellationToken cancellationToken)
    {
        var edgeApplication = new EdgeApplication
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                Version = request.Version,
                Image = request.Image,
                Status = request.Status,

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

        return Results.Created($"/api/edgeApplications/edgeApplication.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateEdgeApplicationRequest request,
        IEdgeApplicationService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new EdgeApplication
        {
            Id = id,
            Name = request.Name,
            Version = request.Version,
            Image = request.Image,
            Status = request.Status,

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
        IEdgeApplicationService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static EdgeApplicationResponse ToResponse(EdgeApplication lowercaseClassName)
        => new( edgeApplication.Id,
                , String, String, String, DeploymentStatus
                , GatewayId );

}
