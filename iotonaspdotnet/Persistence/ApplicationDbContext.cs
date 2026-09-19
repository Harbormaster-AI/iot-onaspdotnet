using Microsoft.EntityFrameworkCore;

using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

                        public DbSet<DeviceVendor> DeviceVendors => Set<DeviceVendor>();
                    public DbSet<HardwareModule> HardwareModules => Set<HardwareModule>();
                    public DbSet<DeviceModel> DeviceModels => Set<DeviceModel>();
                    public DbSet<FirmwareRelease> FirmwareReleases => Set<FirmwareRelease>();
                    public DbSet<IoTDevice> IoTDevices => Set<IoTDevice>();
                    public DbSet<SensorInstance> SensorInstances => Set<SensorInstance>();
                    public DbSet<ActuatorInstance> ActuatorInstances => Set<ActuatorInstance>();
                    public DbSet<TelemetrySchema> TelemetrySchemas => Set<TelemetrySchema>();
                    public DbSet<TelemetryStream> TelemetryStreams => Set<TelemetryStream>();
                    public DbSet<CommandDefinition> CommandDefinitions => Set<CommandDefinition>();
                    public DbSet<CommandInvocation> CommandInvocations => Set<CommandInvocation>();
                    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
                    public DbSet<Alert> Alerts => Set<Alert>();
                    public DbSet<Tenant> Tenants => Set<Tenant>();
                    public DbSet<TenantUser> TenantUsers => Set<TenantUser>();
                    public DbSet<Site> Sites => Set<Site>();
                    public DbSet<Building> Buildings => Set<Building>();
                    public DbSet<Floor> Floors => Set<Floor>();
                    public DbSet<Room> Rooms => Set<Room>();
                    public DbSet<Gateway> Gateways => Set<Gateway>();
                    public DbSet<EdgeApplication> EdgeApplications => Set<EdgeApplication>();
                    public DbSet<NetworkProfile> NetworkProfiles => Set<NetworkProfile>();
                    public DbSet<SimCard> SimCards => Set<SimCard>();
                    public DbSet<ConnectivityPlan> ConnectivityPlans => Set<ConnectivityPlan>();
                    public DbSet<MessagingEndpoint> MessagingEndpoints => Set<MessagingEndpoint>();
                    public DbSet<AccessPolicy> AccessPolicys => Set<AccessPolicy>();
                    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
                    public DbSet<DeviceCertificate> DeviceCertificates => Set<DeviceCertificate>();
                    public DbSet<ProvisioningRecord> ProvisioningRecords => Set<ProvisioningRecord>();
                    public DbSet<DigitalTwin> DigitalTwins => Set<DigitalTwin>();
                    public DbSet<TwinTemplate> TwinTemplates => Set<TwinTemplate>();
                    public DbSet<TwinChangeEvent> TwinChangeEvents => Set<TwinChangeEvent>();
                    public DbSet<MaintenanceTicket> MaintenanceTickets => Set<MaintenanceTicket>();
                    public DbSet<DataRetentionPolicy> DataRetentionPolicys => Set<DataRetentionPolicy>();
                    public DbSet<SoftwareUpdateCampaign> SoftwareUpdateCampaigns => Set<SoftwareUpdateCampaign>();
                    public DbSet<SoftwareUpdateExecution> SoftwareUpdateExecutions => Set<SoftwareUpdateExecution>();
                    public DbSet<DeviceGroup> DeviceGroups => Set<DeviceGroup>();
                    public DbSet<UsageRecord> UsageRecords => Set<UsageRecord>();
            
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // ModuleType has one or more DeviceModels of type {childName}
        modelBuilder.Entity<DeviceModel>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.DeviceModels)
            .HasForeignKey("DeviceModelsId");

        // ModuleType has one or more FirmwareReleases of type {childName}
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("FirmwareReleasesId");

        // ModuleType has one or more HardwareModules of type {childName}
        modelBuilder.Entity<HardwareModule>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("HardwareModulesId");

        // ModuleType has one Vendor of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("VendorId");


        // ModuleType has one Vendor of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("VendorId");

        // ModuleType has one TwinTemplate of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.TwinTemplate)
            .WithMany()
            .HasForeignKey("TwinTemplateId");


        // ModuleType has one or more HardwareModules of type {childName}
        modelBuilder.Entity<HardwareModule>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("HardwareModulesId");

        // ModuleType has one or more FirmwareReleases of type {childName}
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("FirmwareReleasesId");

        // ModuleType has one or more CommandDefinitions of type {childName}
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.CommandDefinitions)
            .HasForeignKey("CommandDefinitionsId");

        // ModuleType has one DeviceModel of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");


        // ModuleType has one DeviceModel of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // ModuleType has one Site of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");

        // ModuleType has one Room of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("RoomId");

        // ModuleType has one Gateway of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // ModuleType has one DigitalTwin of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwinId");

        // ModuleType has one ProvisioningRecord of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.ProvisioningRecord)
            .WithMany()
            .HasForeignKey("ProvisioningRecordId");


        // ModuleType has one or more Sensors of type {childName}
        modelBuilder.Entity<SensorInstance>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Sensors)
            .HasForeignKey("SensorsId");

        // ModuleType has one or more Actuators of type {childName}
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("ActuatorsId");

        // ModuleType has one or more Certificates of type {childName}
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("CertificatesId");

        // ModuleType has one or more TelemetryStreams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("TelemetryStreamsId");

        // ModuleType has one or more CommandInvocations of type {childName}
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // ModuleType has one or more Alerts of type {childName}
        modelBuilder.Entity<Alert>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("AlertsId");

        // ModuleType has one or more DeviceGroups of type {childName}
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("DeviceGroupsId");

        // ModuleType has one or more NetworkProfiles of type {childName}
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // ModuleType has one or more TelemetryStreams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("TelemetryStreamsId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // ModuleType has one or more SupportedCommands of type {childName}
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.SupportedCommands)
            .HasForeignKey("SupportedCommandsId");


        // ModuleType has one or more Streams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Sensor of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Sensor)
            .WithMany()
            .HasForeignKey("SensorId");

        // ModuleType has one Schema of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Schema)
            .WithMany()
            .HasForeignKey("SchemaId");

        // ModuleType has one MessagingEndpoint of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.MessagingEndpoint)
            .WithMany()
            .HasForeignKey("MessagingEndpointId");

        // ModuleType has one RetentionPolicy of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.RetentionPolicy)
            .WithMany()
            .HasForeignKey("RetentionPolicyId");


        // ModuleType has one DeviceModel of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");


        // ModuleType has one or more Actuators of type {childName}
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("ActuatorsId");

        // ModuleType has one or more CommandInvocations of type {childName}
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one CommandDefinition of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.CommandDefinition)
            .WithMany()
            .HasForeignKey("CommandDefinitionId");

        // ModuleType has one Actuator of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Actuator)
            .WithMany()
            .HasForeignKey("ActuatorId");

        // ModuleType has one User of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey("UserId");


        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more Streams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // ModuleType has one or more Alerts of type {childName}
        modelBuilder.Entity<Alert>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("AlertsId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one AlertRule of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.AlertRule)
            .WithMany()
            .HasForeignKey("AlertRuleId");



        // ModuleType has one or more Sites of type {childName}
        modelBuilder.Entity<Site>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Sites)
            .HasForeignKey("SitesId");

        // ModuleType has one or more Users of type {childName}
        modelBuilder.Entity<TenantUser>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // ModuleType has one or more Devices of type {childName}
        modelBuilder.Entity<IoTDevice>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // ModuleType has one or more DataRetentionPolicies of type {childName}
        modelBuilder.Entity<DataRetentionPolicy>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.DataRetentionPolicies)
            .HasForeignKey("DataRetentionPoliciesId");

        // ModuleType has one or more ConnectivityPlans of type {childName}
        modelBuilder.Entity<ConnectivityPlan>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.ConnectivityPlans)
            .HasForeignKey("ConnectivityPlansId");

        // ModuleType has one or more SimCards of type {childName}
        modelBuilder.Entity<SimCard>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("SimCardsId");

        // ModuleType has one or more MessagingEndpoints of type {childName}
        modelBuilder.Entity<MessagingEndpoint>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.MessagingEndpoints)
            .HasForeignKey("MessagingEndpointsId");

        // ModuleType has one or more AccessPolicies of type {childName}
        modelBuilder.Entity<AccessPolicy>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.AccessPolicies)
            .HasForeignKey("AccessPoliciesId");

        // ModuleType has one or more DeviceGroups of type {childName}
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("DeviceGroupsId");

        // ModuleType has one or more AlertRules of type {childName}
        modelBuilder.Entity<AlertRule>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.AlertRules)
            .HasForeignKey("AlertRulesId");

        // ModuleType has one or more MaintenanceTickets of type {childName}
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.MaintenanceTickets)
            .HasForeignKey("MaintenanceTicketsId");

        // ModuleType has one or more UsageRecords of type {childName}
        modelBuilder.Entity<UsageRecord>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.UsageRecords)
            .HasForeignKey("UsageRecordsId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more CommandInvocations of type {childName}
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more Buildings of type {childName}
        modelBuilder.Entity<Building>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Buildings)
            .HasForeignKey("BuildingsId");

        // ModuleType has one or more Devices of type {childName}
        modelBuilder.Entity<IoTDevice>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // ModuleType has one or more Gateways of type {childName}
        modelBuilder.Entity<Gateway>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("GatewaysId");

        // ModuleType has one Site of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");


        // ModuleType has one or more Floors of type {childName}
        modelBuilder.Entity<Floor>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Floors)
            .HasForeignKey("FloorsId");

        // ModuleType has one Building of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Building)
            .WithMany()
            .HasForeignKey("BuildingId");


        // ModuleType has one or more Rooms of type {childName}
        modelBuilder.Entity<Room>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Rooms)
            .HasForeignKey("RoomsId");

        // ModuleType has one Floor of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Floor)
            .WithMany()
            .HasForeignKey("FloorId");


        // ModuleType has one or more Devices of type {childName}
        modelBuilder.Entity<IoTDevice>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // ModuleType has one or more Gateways of type {childName}
        modelBuilder.Entity<Gateway>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("GatewaysId");

        // ModuleType has one Site of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");

        // ModuleType has one Room of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("RoomId");

        // ModuleType has one DigitalTwin of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwinId");


        // ModuleType has one or more Devices of type {childName}
        modelBuilder.Entity<IoTDevice>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // ModuleType has one or more EdgeApplications of type {childName}
        modelBuilder.Entity<EdgeApplication>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.EdgeApplications)
            .HasForeignKey("EdgeApplicationsId");

        // ModuleType has one or more Certificates of type {childName}
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("CertificatesId");

        // ModuleType has one or more NetworkProfiles of type {childName}
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // ModuleType has one Gateway of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");


        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Gateway of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // ModuleType has one SimCard of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.SimCard)
            .WithMany()
            .HasForeignKey("SimCardId");


        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // ModuleType has one ConnectivityPlan of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlanId");


        // ModuleType has one or more NetworkProfiles of type {childName}
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more SimCards of type {childName}
        modelBuilder.Entity<SimCard>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("SimCardsId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more Streams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more ApiKeys of type {childName}
        modelBuilder.Entity<ApiKey>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.ApiKeys)
            .HasForeignKey("ApiKeysId");

        // ModuleType has one or more Users of type {childName}
        modelBuilder.Entity<TenantUser>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // ModuleType has one AccessPolicy of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.AccessPolicy)
            .WithMany()
            .HasForeignKey("AccessPolicyId");


        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Gateway of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");


        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Certificate of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Certificate)
            .WithMany()
            .HasForeignKey("CertificateId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Gateway of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // ModuleType has one Template of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey("TemplateId");


        // ModuleType has one or more ChangeEvents of type {childName}
        modelBuilder.Entity<TwinChangeEvent>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.ChangeEvents)
            .HasForeignKey("ChangeEventsId");


        // ModuleType has one or more DeviceModels of type {childName}
        modelBuilder.Entity<DeviceModel>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.DeviceModels)
            .HasForeignKey("DeviceModelsId");

        // ModuleType has one Twin of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Twin)
            .WithMany()
            .HasForeignKey("TwinId");


        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more Streams of type {childName}
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // ModuleType has one FirmwareRelease of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.FirmwareRelease)
            .WithMany()
            .HasForeignKey("FirmwareReleaseId");

        // ModuleType has one DeviceGroup of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.DeviceGroup)
            .WithMany()
            .HasForeignKey("DeviceGroupId");


        // ModuleType has one or more Executions of type {childName}
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Executions)
            .HasForeignKey("ExecutionsId");

        // ModuleType has one Campaign of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ModuleType has one or more Devices of type {childName}
        modelBuilder.Entity<IoTDevice>()
            .HasOne<ModuleType>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // ModuleType has one Tenant of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // ModuleType has one Device of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ModuleType has one ConnectivityPlan of type {childName}
        modelBuilder.Entity<ModuleType>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlanId");


    }
}
