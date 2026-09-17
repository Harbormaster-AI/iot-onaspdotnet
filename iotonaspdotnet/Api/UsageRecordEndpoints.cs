using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


namespace iotonaspdotnet.Api;

public static class UsageRecordEndpoints
{
    public static IEndpointRouteBuilder MapUsageRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usageRecord").WithTags("UsageRecords");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapDelete("/", assignTenant);
        group.MapDelete("/", unassignTenant);
        group.MapDelete("/", assignDevice);
        group.MapDelete("/", unassignDevice);
        group.MapDelete("/", assignConnectivityPlan);
        group.MapDelete("/", unassignConnectivityPlan);


        return app;
    }

    private static async Task<IResult> Create(
        UsageRecordRequest request,
        IUsageRecordService service,
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
        UsageRecordRequest request,
        IUsageRecordService service,
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
        IUsageRecordService service,
        CancellationToken cancellationToken) {

        var all; = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( UsageRecordResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IUsageRecordService service,
        CancellationToken cancellationToken) {

        var usageRecord = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return usageRecord is null ? Results.NotFound() : Results.Ok( usageRecord );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignTenantAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignTenantAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignDeviceAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assign = await service.AssignConnectivityPlanAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var deleted = await service.AssignConnectivityPlanAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


}
