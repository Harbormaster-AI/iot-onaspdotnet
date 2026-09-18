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

        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);
        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignConnectivityPlan);
        group.MapPut("/", unassignConnectivityPlan);


        return app;
    }

    private static async Task<IResult> Create(
        UsageRecordRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(lowercaseClassName, cancellationToken);
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
        IUsageRecordService service,
        CancellationToken cancellationToken) {

        var usageRecord = await service.Get(identifier, cancellationToken);
        return usageRecord is null ? Results.NotFound() : Results.Ok( usageRecord );
    }


    private static async Task<IResult> GetAll(
        IUsageRecordService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( UsageRecordResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlan(
        AssociationRequest request,
        IUsageRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignConnectivityPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignConnectivityPlan(
    AssociationRequest request,
    IUsageRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignConnectivityPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private UsageRecord mapRequestToUsageRecord( UsageRecordRequest request ) {
        var model = new UsageRecord
        {
            Id = request.id,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            MessagesSent = request.MessagesSent,
            DataVolumeMB = request.DataVolumeMB,
            Tenant = request.Tenant,
            Device = request.Device,
            ConnectivityPlan = request.ConnectivityPlan,
        };
        return model;
    }

}
