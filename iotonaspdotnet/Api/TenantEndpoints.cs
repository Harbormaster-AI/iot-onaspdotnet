using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;


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


    group.MapDelete("/", addToSites);
    group.MapDelete("/", removeFromSites);

    group.MapDelete("/", addToUsers);
    group.MapDelete("/", removeFromUsers);

    group.MapDelete("/", addToDevices);
    group.MapDelete("/", removeFromDevices);

    group.MapDelete("/", addToDataRetentionPolicies);
    group.MapDelete("/", removeFromDataRetentionPolicies);

    group.MapDelete("/", addToConnectivityPlans);
    group.MapDelete("/", removeFromConnectivityPlans);

    group.MapDelete("/", addToSimCards);
    group.MapDelete("/", removeFromSimCards);

    group.MapDelete("/", addToMessagingEndpoints);
    group.MapDelete("/", removeFromMessagingEndpoints);

    group.MapDelete("/", addToAccessPolicies);
    group.MapDelete("/", removeFromAccessPolicies);

    group.MapDelete("/", addToDeviceGroups);
    group.MapDelete("/", removeFromDeviceGroups);

    group.MapDelete("/", addToAlertRules);
    group.MapDelete("/", removeFromAlertRules);

    group.MapDelete("/", addToMaintenanceTickets);
    group.MapDelete("/", removeFromMaintenanceTickets);

    group.MapDelete("/", addToUsageRecords);
    group.MapDelete("/", removeFromUsageRecords);


        return app;
    }

    private static async Task<IResult> Create(
        TenantRequest request,
        ITenantService service,
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
        TenantRequest request,
        ITenantService service,
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
        ITenantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAllAsync(cancellationToken);
        return Results.Ok( all.Select( TenantResponse.FromModel ) );
    }

    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITenantService service,
        CancellationToken cancellationToken) {

        var tenant = await service.GetByIdAsync(identifier.Id, cancellationToken);
        return tenant is null ? Results.NotFound() : Results.Ok( tenant );
    }


    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.DeleteAsync(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AssignSites(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToSitesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSites(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromSitesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignUsers(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToUsersAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUsers(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromUsersAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDevicesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevices(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDevicesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignDataRetentionPolicies(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDataRetentionPoliciesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDataRetentionPolicies(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDataRetentionPoliciesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignConnectivityPlans(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToConnectivityPlansAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignConnectivityPlans(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromConnectivityPlansAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignSimCards(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToSimCardsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSimCards(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromSimCardsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignMessagingEndpoints(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToMessagingEndpointsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMessagingEndpoints(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromMessagingEndpointsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignAccessPolicies(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToAccessPoliciesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccessPolicies(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromAccessPoliciesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignDeviceGroups(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToDeviceGroupsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDeviceGroups(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromDeviceGroupsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignAlertRules(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToAlertRulesAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAlertRules(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromAlertRulesAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignMaintenanceTickets(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToMaintenanceTicketsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMaintenanceTickets(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromMaintenanceTicketsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
    private static async Task<IResult> AssignUsageRecords(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var assign = await service.AddToUsageRecordsAsync(request.Id, cancellationToken);
        return assign ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUsageRecords(
        AssociationRequest request,
        ITenantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.RemoveFromUsageRecordsAsync(request.Id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private Tenant mapRequestToTenant( TenantRequest request ) {
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
        }
        return model;
    }
}
