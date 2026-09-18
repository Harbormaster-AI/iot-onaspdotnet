using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class TenantEndpoints
{
    public static IEndpointRouteBuilder MapTenantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tenant").WithTags("Tenants");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);


    group.MapPut("/", addToSites);
    group.MapPut("/", removeFromSites);

    group.MapPut("/", addToUsers);
    group.MapPut("/", removeFromUsers);

    group.MapPut("/", addToDevices);
    group.MapPut("/", removeFromDevices);

    group.MapPut("/", addToDataRetentionPolicies);
    group.MapPut("/", removeFromDataRetentionPolicies);

    group.MapPut("/", addToConnectivityPlans);
    group.MapPut("/", removeFromConnectivityPlans);

    group.MapPut("/", addToSimCards);
    group.MapPut("/", removeFromSimCards);

    group.MapPut("/", addToMessagingEndpoints);
    group.MapPut("/", removeFromMessagingEndpoints);

    group.MapPut("/", addToAccessPolicies);
    group.MapPut("/", removeFromAccessPolicies);

    group.MapPut("/", addToDeviceGroups);
    group.MapPut("/", removeFromDeviceGroups);

    group.MapPut("/", addToAlertRules);
    group.MapPut("/", removeFromAlertRules);

    group.MapPut("/", addToMaintenanceTickets);
    group.MapPut("/", removeFromMaintenanceTickets);

    group.MapPut("/", addToUsageRecords);
    group.MapPut("/", removeFromUsageRecords);


        return app;
    }

    private static async Task<IResult> Create(
        TenantRequest request,
        ITenantService service,
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
        TenantRequest request,
        ITenantService service,
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
        ITenantService service,
        CancellationToken cancellationToken) {

        var tenant = await service.Get(identifier, cancellationToken);
        return tenant is null ? Results.NotFound() : Results.Ok( tenant );
    }


    private static async Task<IResult> GetAll(
        ITenantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TenantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSites(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSites(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSites(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSites(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToUsers(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUsers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUsers(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUsers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDevices(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDevices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDevices(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDevices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDataRetentionPolicies(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDataRetentionPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDataRetentionPolicies(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDataRetentionPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToConnectivityPlans(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConnectivityPlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConnectivityPlans(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConnectivityPlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSimCards(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSimCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSimCards(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSimCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMessagingEndpoints(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMessagingEndpoints(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMessagingEndpoints(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMessagingEndpoints(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAccessPolicies(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAccessPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAccessPolicies(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAccessPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDeviceGroups(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDeviceGroups(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDeviceGroups(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDeviceGroups(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAlertRules(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAlertRules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAlertRules(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAlertRules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMaintenanceTickets(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMaintenanceTickets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMaintenanceTickets(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMaintenanceTickets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToUsageRecords(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUsageRecords(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUsageRecords(
        MultipleAssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUsageRecords(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Tenant mapRequestToTenant( TenantRequest request ) {
        var model = new Tenant
        {
            Id = request.id,
            Name = request.Name,
            Sites = request.Sites,
            Users = request.Users,
            Devices = request.Devices,
            DataRetentionPolicies = request.DataRetentionPolicies,
            ConnectivityPlans = request.ConnectivityPlans,
            SimCards = request.SimCards,
            MessagingEndpoints = request.MessagingEndpoints,
            AccessPolicies = request.AccessPolicies,
            DeviceGroups = request.DeviceGroups,
            AlertRules = request.AlertRules,
            MaintenanceTickets = request.MaintenanceTickets,
            UsageRecords = request.UsageRecords,
            TenantType = request.TenantType,
        };
        return model;
    }

}
