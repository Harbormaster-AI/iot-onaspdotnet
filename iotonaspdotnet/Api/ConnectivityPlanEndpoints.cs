using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class ConnectivityPlanEndpoints
{
    public static IEndpointRouteBuilder MapConnectivityPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/connectivityPlan").WithTags("ConnectivityPlans");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IConnectivityPlanService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IConnectivityPlanService service,
        CancellationToken cancellationToken)
    {
        var connectivityPlan = await service.GetByIdAsync(id, cancellationToken);
        return connectivityPlan is null ? Results.NotFound() : Results.Ok(ToResponse( connectivityPlan ));
    }

    private static async Task<IResult> Create(
        CreateConnectivityPlanRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken)
    {
        var connectivityPlan = new ConnectivityPlan
        {
            Id = Guid.NewGuid(),

                Name = request.Name,
                DataCapMB = request.DataCapMB,
                BillingCycleDays = request.BillingCycleDays,

                TenantId = request.TenantId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/connectivityPlans/connectivityPlan.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateConnectivityPlanRequest request,
        IConnectivityPlanService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new ConnectivityPlan
        {
            Id = id,
            Name = request.Name,
            DataCapMB = request.DataCapMB,
            BillingCycleDays = request.BillingCycleDays,

            TenantId = request.TenantId,
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
        IConnectivityPlanService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static ConnectivityPlanResponse ToResponse(ConnectivityPlan lowercaseClassName)
        => new( connectivityPlan.Id,
                , String, Integer, Integer
                , TenantId );

}
