using iotonaspdotnet.Api;
using iotonaspdotnet.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

// MySQL for local/runtime; SQLite is used automatically when environment is Testing.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Tests set environment "Testing" and use SQLite so `dotnet test` needs no MySQL.
    if (builder.Environment.IsEnvironment("Testing"))
    {
        var sqlitePath = Path.Combine(Path.GetTempPath(), "iotonaspdotnet-testing.db");
        options.UseSqlite($"Data Source={sqlitePath}");
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
        options.UseMySql(connectionString, ServerVersion.Parse("8.4.0-mysql"));
    }
});

                        builder.Services.AddScoped<IDeviceVendorRepository, DeviceVendorRepository>();
                    builder.Services.AddScoped<IHardwareModuleRepository, HardwareModuleRepository>();
                    builder.Services.AddScoped<IDeviceModelRepository, DeviceModelRepository>();
                    builder.Services.AddScoped<IFirmwareReleaseRepository, FirmwareReleaseRepository>();
                    builder.Services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
                    builder.Services.AddScoped<ISensorInstanceRepository, SensorInstanceRepository>();
                    builder.Services.AddScoped<IActuatorInstanceRepository, ActuatorInstanceRepository>();
                    builder.Services.AddScoped<ITelemetrySchemaRepository, TelemetrySchemaRepository>();
                    builder.Services.AddScoped<ITelemetryStreamRepository, TelemetryStreamRepository>();
                    builder.Services.AddScoped<ICommandDefinitionRepository, CommandDefinitionRepository>();
                    builder.Services.AddScoped<ICommandInvocationRepository, CommandInvocationRepository>();
                    builder.Services.AddScoped<IAlertRuleRepository, AlertRuleRepository>();
                    builder.Services.AddScoped<IAlertRepository, AlertRepository>();
                    builder.Services.AddScoped<ITenantRepository, TenantRepository>();
                    builder.Services.AddScoped<ITenantUserRepository, TenantUserRepository>();
                    builder.Services.AddScoped<ISiteRepository, SiteRepository>();
                    builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
                    builder.Services.AddScoped<IFloorRepository, FloorRepository>();
                    builder.Services.AddScoped<IRoomRepository, RoomRepository>();
                    builder.Services.AddScoped<IGatewayRepository, GatewayRepository>();
                    builder.Services.AddScoped<IEdgeApplicationRepository, EdgeApplicationRepository>();
                    builder.Services.AddScoped<INetworkProfileRepository, NetworkProfileRepository>();
                    builder.Services.AddScoped<ISimCardRepository, SimCardRepository>();
                    builder.Services.AddScoped<IConnectivityPlanRepository, ConnectivityPlanRepository>();
                    builder.Services.AddScoped<IMessagingEndpointRepository, MessagingEndpointRepository>();
                    builder.Services.AddScoped<IAccessPolicyRepository, AccessPolicyRepository>();
                    builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
                    builder.Services.AddScoped<IDeviceCertificateRepository, DeviceCertificateRepository>();
                    builder.Services.AddScoped<IProvisioningRecordRepository, ProvisioningRecordRepository>();
                    builder.Services.AddScoped<IDigitalTwinRepository, DigitalTwinRepository>();
                    builder.Services.AddScoped<ITwinTemplateRepository, TwinTemplateRepository>();
                    builder.Services.AddScoped<ITwinChangeEventRepository, TwinChangeEventRepository>();
                    builder.Services.AddScoped<IMaintenanceTicketRepository, MaintenanceTicketRepository>();
                    builder.Services.AddScoped<IDataRetentionPolicyRepository, DataRetentionPolicyRepository>();
                    builder.Services.AddScoped<ISoftwareUpdateCampaignRepository, SoftwareUpdateCampaignRepository>();
                    builder.Services.AddScoped<ISoftwareUpdateExecutionRepository, SoftwareUpdateExecutionRepository>();
                    builder.Services.AddScoped<IDeviceGroupRepository, DeviceGroupRepository>();
                    builder.Services.AddScoped<IUsageRecordRepository, UsageRecordRepository>();
            
                        builder.Services.AddScoped<IDeviceVendorService, DeviceVendorService>();
                    builder.Services.AddScoped<IHardwareModuleService, HardwareModuleService>();
                    builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
                    builder.Services.AddScoped<IFirmwareReleaseService, FirmwareReleaseService>();
                    builder.Services.AddScoped<IIoTDeviceService, IoTDeviceService>();
                    builder.Services.AddScoped<ISensorInstanceService, SensorInstanceService>();
                    builder.Services.AddScoped<IActuatorInstanceService, ActuatorInstanceService>();
                    builder.Services.AddScoped<ITelemetrySchemaService, TelemetrySchemaService>();
                    builder.Services.AddScoped<ITelemetryStreamService, TelemetryStreamService>();
                    builder.Services.AddScoped<ICommandDefinitionService, CommandDefinitionService>();
                    builder.Services.AddScoped<ICommandInvocationService, CommandInvocationService>();
                    builder.Services.AddScoped<IAlertRuleService, AlertRuleService>();
                    builder.Services.AddScoped<IAlertService, AlertService>();
                    builder.Services.AddScoped<ITenantService, TenantService>();
                    builder.Services.AddScoped<ITenantUserService, TenantUserService>();
                    builder.Services.AddScoped<ISiteService, SiteService>();
                    builder.Services.AddScoped<IBuildingService, BuildingService>();
                    builder.Services.AddScoped<IFloorService, FloorService>();
                    builder.Services.AddScoped<IRoomService, RoomService>();
                    builder.Services.AddScoped<IGatewayService, GatewayService>();
                    builder.Services.AddScoped<IEdgeApplicationService, EdgeApplicationService>();
                    builder.Services.AddScoped<INetworkProfileService, NetworkProfileService>();
                    builder.Services.AddScoped<ISimCardService, SimCardService>();
                    builder.Services.AddScoped<IConnectivityPlanService, ConnectivityPlanService>();
                    builder.Services.AddScoped<IMessagingEndpointService, MessagingEndpointService>();
                    builder.Services.AddScoped<IAccessPolicyService, AccessPolicyService>();
                    builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
                    builder.Services.AddScoped<IDeviceCertificateService, DeviceCertificateService>();
                    builder.Services.AddScoped<IProvisioningRecordService, ProvisioningRecordService>();
                    builder.Services.AddScoped<IDigitalTwinService, DigitalTwinService>();
                    builder.Services.AddScoped<ITwinTemplateService, TwinTemplateService>();
                    builder.Services.AddScoped<ITwinChangeEventService, TwinChangeEventService>();
                    builder.Services.AddScoped<IMaintenanceTicketService, MaintenanceTicketService>();
                    builder.Services.AddScoped<IDataRetentionPolicyService, DataRetentionPolicyService>();
                    builder.Services.AddScoped<ISoftwareUpdateCampaignService, SoftwareUpdateCampaignService>();
                    builder.Services.AddScoped<ISoftwareUpdateExecutionService, SoftwareUpdateExecutionService>();
                    builder.Services.AddScoped<IDeviceGroupService, DeviceGroupService>();
                    builder.Services.AddScoped<IUsageRecordService, UsageRecordService>();
            

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

                        app.MapDeviceVendorEndpoints();
                    app.MapHardwareModuleEndpoints();
                    app.MapDeviceModelEndpoints();
                    app.MapFirmwareReleaseEndpoints();
                    app.MapIoTDeviceEndpoints();
                    app.MapSensorInstanceEndpoints();
                    app.MapActuatorInstanceEndpoints();
                    app.MapTelemetrySchemaEndpoints();
                    app.MapTelemetryStreamEndpoints();
                    app.MapCommandDefinitionEndpoints();
                    app.MapCommandInvocationEndpoints();
                    app.MapAlertRuleEndpoints();
                    app.MapAlertEndpoints();
                    app.MapTenantEndpoints();
                    app.MapTenantUserEndpoints();
                    app.MapSiteEndpoints();
                    app.MapBuildingEndpoints();
                    app.MapFloorEndpoints();
                    app.MapRoomEndpoints();
                    app.MapGatewayEndpoints();
                    app.MapEdgeApplicationEndpoints();
                    app.MapNetworkProfileEndpoints();
                    app.MapSimCardEndpoints();
                    app.MapConnectivityPlanEndpoints();
                    app.MapMessagingEndpointEndpoints();
                    app.MapAccessPolicyEndpoints();
                    app.MapApiKeyEndpoints();
                    app.MapDeviceCertificateEndpoints();
                    app.MapProvisioningRecordEndpoints();
                    app.MapDigitalTwinEndpoints();
                    app.MapTwinTemplateEndpoints();
                    app.MapTwinChangeEventEndpoints();
                    app.MapMaintenanceTicketEndpoints();
                    app.MapDataRetentionPolicyEndpoints();
                    app.MapSoftwareUpdateCampaignEndpoints();
                    app.MapSoftwareUpdateExecutionEndpoints();
                    app.MapDeviceGroupEndpoints();
                    app.MapUsageRecordEndpoints();
            
app.Run();

public partial class Program { }
