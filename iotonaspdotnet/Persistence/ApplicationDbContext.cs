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


        // DeviceVendor has one or more DeviceModels of type DeviceModel
        modelBuilder.Entity<DeviceModel>()
            .HasOne<DeviceVendor>()
            .WithMany(parent => parent.DeviceModels)
            .HasForeignKey("DeviceModelsId");

        // DeviceVendor has one or more FirmwareReleases of type FirmwareRelease
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<DeviceVendor>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("FirmwareReleasesId");

        // DeviceVendor has one or more HardwareModules of type HardwareModule
        modelBuilder.Entity<HardwareModule>()
            .HasOne<DeviceVendor>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("HardwareModulesId");

        // HardwareModule has one Vendor of type DeviceVendor
        modelBuilder.Entity<HardwareModule>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("VendorId");


        // DeviceModel has one Vendor of type DeviceVendor
        modelBuilder.Entity<DeviceModel>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("VendorId");

        // DeviceModel has one TwinTemplate of type TwinTemplate
        modelBuilder.Entity<DeviceModel>()
            .HasOne(x => x.TwinTemplate)
            .WithMany()
            .HasForeignKey("TwinTemplateId");


        // DeviceModel has one or more HardwareModules of type HardwareModule
        modelBuilder.Entity<HardwareModule>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("HardwareModulesId");

        // DeviceModel has one or more FirmwareReleases of type FirmwareRelease
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("FirmwareReleasesId");

        // DeviceModel has one or more CommandDefinitions of type CommandDefinition
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.CommandDefinitions)
            .HasForeignKey("CommandDefinitionsId");

        // FirmwareRelease has one DeviceModel of type DeviceModel
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");


        // IoTDevice has one DeviceModel of type DeviceModel
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");

        // IoTDevice has one Tenant of type Tenant
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // IoTDevice has one Site of type Site
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");

        // IoTDevice has one Room of type Room
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("RoomId");

        // IoTDevice has one Gateway of type Gateway
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // IoTDevice has one DigitalTwin of type DigitalTwin
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwinId");

        // IoTDevice has one ProvisioningRecord of type ProvisioningRecord
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.ProvisioningRecord)
            .WithMany()
            .HasForeignKey("ProvisioningRecordId");


        // IoTDevice has one or more Sensors of type SensorInstance
        modelBuilder.Entity<SensorInstance>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Sensors)
            .HasForeignKey("SensorsId");

        // IoTDevice has one or more Actuators of type ActuatorInstance
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("ActuatorsId");

        // IoTDevice has one or more Certificates of type DeviceCertificate
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("CertificatesId");

        // IoTDevice has one or more TelemetryStreams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("TelemetryStreamsId");

        // IoTDevice has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // IoTDevice has one or more Alerts of type Alert
        modelBuilder.Entity<Alert>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("AlertsId");

        // IoTDevice has one or more DeviceGroups of type DeviceGroup
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("DeviceGroupsId");

        // IoTDevice has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // SensorInstance has one Device of type IoTDevice
        modelBuilder.Entity<SensorInstance>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // SensorInstance has one or more TelemetryStreams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<SensorInstance>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("TelemetryStreamsId");

        // ActuatorInstance has one Device of type IoTDevice
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // ActuatorInstance has one or more SupportedCommands of type CommandDefinition
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<ActuatorInstance>()
            .WithMany(parent => parent.SupportedCommands)
            .HasForeignKey("SupportedCommandsId");


        // TelemetrySchema has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<TelemetrySchema>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // TelemetryStream has one Device of type IoTDevice
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // TelemetryStream has one Sensor of type SensorInstance
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Sensor)
            .WithMany()
            .HasForeignKey("SensorId");

        // TelemetryStream has one Schema of type TelemetrySchema
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Schema)
            .WithMany()
            .HasForeignKey("SchemaId");

        // TelemetryStream has one MessagingEndpoint of type MessagingEndpoint
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.MessagingEndpoint)
            .WithMany()
            .HasForeignKey("MessagingEndpointId");

        // TelemetryStream has one RetentionPolicy of type DataRetentionPolicy
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.RetentionPolicy)
            .WithMany()
            .HasForeignKey("RetentionPolicyId");


        // CommandDefinition has one DeviceModel of type DeviceModel
        modelBuilder.Entity<CommandDefinition>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModelId");


        // CommandDefinition has one or more Actuators of type ActuatorInstance
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<CommandDefinition>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("ActuatorsId");

        // CommandDefinition has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<CommandDefinition>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // CommandInvocation has one Device of type IoTDevice
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // CommandInvocation has one CommandDefinition of type CommandDefinition
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.CommandDefinition)
            .WithMany()
            .HasForeignKey("CommandDefinitionId");

        // CommandInvocation has one Actuator of type ActuatorInstance
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.Actuator)
            .WithMany()
            .HasForeignKey("ActuatorId");

        // CommandInvocation has one User of type TenantUser
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey("UserId");


        // AlertRule has one Tenant of type Tenant
        modelBuilder.Entity<AlertRule>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // AlertRule has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<AlertRule>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // AlertRule has one or more Alerts of type Alert
        modelBuilder.Entity<Alert>()
            .HasOne<AlertRule>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("AlertsId");

        // Alert has one Device of type IoTDevice
        modelBuilder.Entity<Alert>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // Alert has one AlertRule of type AlertRule
        modelBuilder.Entity<Alert>()
            .HasOne(x => x.AlertRule)
            .WithMany()
            .HasForeignKey("AlertRuleId");



        // Tenant has one or more Sites of type Site
        modelBuilder.Entity<Site>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Sites)
            .HasForeignKey("SitesId");

        // Tenant has one or more Users of type TenantUser
        modelBuilder.Entity<TenantUser>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // Tenant has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // Tenant has one or more DataRetentionPolicies of type DataRetentionPolicy
        modelBuilder.Entity<DataRetentionPolicy>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.DataRetentionPolicies)
            .HasForeignKey("DataRetentionPoliciesId");

        // Tenant has one or more ConnectivityPlans of type ConnectivityPlan
        modelBuilder.Entity<ConnectivityPlan>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.ConnectivityPlans)
            .HasForeignKey("ConnectivityPlansId");

        // Tenant has one or more SimCards of type SimCard
        modelBuilder.Entity<SimCard>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("SimCardsId");

        // Tenant has one or more MessagingEndpoints of type MessagingEndpoint
        modelBuilder.Entity<MessagingEndpoint>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.MessagingEndpoints)
            .HasForeignKey("MessagingEndpointsId");

        // Tenant has one or more AccessPolicies of type AccessPolicy
        modelBuilder.Entity<AccessPolicy>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.AccessPolicies)
            .HasForeignKey("AccessPoliciesId");

        // Tenant has one or more DeviceGroups of type DeviceGroup
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("DeviceGroupsId");

        // Tenant has one or more AlertRules of type AlertRule
        modelBuilder.Entity<AlertRule>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.AlertRules)
            .HasForeignKey("AlertRulesId");

        // Tenant has one or more MaintenanceTickets of type MaintenanceTicket
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.MaintenanceTickets)
            .HasForeignKey("MaintenanceTicketsId");

        // Tenant has one or more UsageRecords of type UsageRecord
        modelBuilder.Entity<UsageRecord>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.UsageRecords)
            .HasForeignKey("UsageRecordsId");

        // TenantUser has one Tenant of type Tenant
        modelBuilder.Entity<TenantUser>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // TenantUser has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<TenantUser>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandInvocationsId");

        // Site has one Tenant of type Tenant
        modelBuilder.Entity<Site>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // Site has one or more Buildings of type Building
        modelBuilder.Entity<Building>()
            .HasOne<Site>()
            .WithMany(parent => parent.Buildings)
            .HasForeignKey("BuildingsId");

        // Site has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Site>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // Site has one or more Gateways of type Gateway
        modelBuilder.Entity<Gateway>()
            .HasOne<Site>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("GatewaysId");

        // Building has one Site of type Site
        modelBuilder.Entity<Building>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");


        // Building has one or more Floors of type Floor
        modelBuilder.Entity<Floor>()
            .HasOne<Building>()
            .WithMany(parent => parent.Floors)
            .HasForeignKey("FloorsId");

        // Floor has one Building of type Building
        modelBuilder.Entity<Floor>()
            .HasOne(x => x.Building)
            .WithMany()
            .HasForeignKey("BuildingId");


        // Floor has one or more Rooms of type Room
        modelBuilder.Entity<Room>()
            .HasOne<Floor>()
            .WithMany(parent => parent.Rooms)
            .HasForeignKey("RoomsId");

        // Room has one Floor of type Floor
        modelBuilder.Entity<Room>()
            .HasOne(x => x.Floor)
            .WithMany()
            .HasForeignKey("FloorId");


        // Room has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Room>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // Room has one or more Gateways of type Gateway
        modelBuilder.Entity<Gateway>()
            .HasOne<Room>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("GatewaysId");

        // Gateway has one Site of type Site
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("SiteId");

        // Gateway has one Room of type Room
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("RoomId");

        // Gateway has one DigitalTwin of type DigitalTwin
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwinId");


        // Gateway has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // Gateway has one or more EdgeApplications of type EdgeApplication
        modelBuilder.Entity<EdgeApplication>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.EdgeApplications)
            .HasForeignKey("EdgeApplicationsId");

        // Gateway has one or more Certificates of type DeviceCertificate
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("CertificatesId");

        // Gateway has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // EdgeApplication has one Gateway of type Gateway
        modelBuilder.Entity<EdgeApplication>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");


        // NetworkProfile has one Device of type IoTDevice
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // NetworkProfile has one Gateway of type Gateway
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // NetworkProfile has one SimCard of type SimCard
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.SimCard)
            .WithMany()
            .HasForeignKey("SimCardId");


        // SimCard has one Tenant of type Tenant
        modelBuilder.Entity<SimCard>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // SimCard has one ConnectivityPlan of type ConnectivityPlan
        modelBuilder.Entity<SimCard>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlanId");


        // SimCard has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<SimCard>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("NetworkProfilesId");

        // ConnectivityPlan has one Tenant of type Tenant
        modelBuilder.Entity<ConnectivityPlan>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // ConnectivityPlan has one or more SimCards of type SimCard
        modelBuilder.Entity<SimCard>()
            .HasOne<ConnectivityPlan>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("SimCardsId");

        // MessagingEndpoint has one Tenant of type Tenant
        modelBuilder.Entity<MessagingEndpoint>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // MessagingEndpoint has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<MessagingEndpoint>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // AccessPolicy has one Tenant of type Tenant
        modelBuilder.Entity<AccessPolicy>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // AccessPolicy has one or more ApiKeys of type ApiKey
        modelBuilder.Entity<ApiKey>()
            .HasOne<AccessPolicy>()
            .WithMany(parent => parent.ApiKeys)
            .HasForeignKey("ApiKeysId");

        // AccessPolicy has one or more Users of type TenantUser
        modelBuilder.Entity<TenantUser>()
            .HasOne<AccessPolicy>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("UsersId");

        // ApiKey has one AccessPolicy of type AccessPolicy
        modelBuilder.Entity<ApiKey>()
            .HasOne(x => x.AccessPolicy)
            .WithMany()
            .HasForeignKey("AccessPolicyId");


        // DeviceCertificate has one Device of type IoTDevice
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // DeviceCertificate has one Gateway of type Gateway
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");


        // ProvisioningRecord has one Device of type IoTDevice
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // ProvisioningRecord has one Certificate of type DeviceCertificate
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Certificate)
            .WithMany()
            .HasForeignKey("CertificateId");

        // ProvisioningRecord has one Tenant of type Tenant
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // DigitalTwin has one Device of type IoTDevice
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // DigitalTwin has one Gateway of type Gateway
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("GatewayId");

        // DigitalTwin has one Template of type TwinTemplate
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey("TemplateId");


        // DigitalTwin has one or more ChangeEvents of type TwinChangeEvent
        modelBuilder.Entity<TwinChangeEvent>()
            .HasOne<DigitalTwin>()
            .WithMany(parent => parent.ChangeEvents)
            .HasForeignKey("ChangeEventsId");


        // TwinTemplate has one or more DeviceModels of type DeviceModel
        modelBuilder.Entity<DeviceModel>()
            .HasOne<TwinTemplate>()
            .WithMany(parent => parent.DeviceModels)
            .HasForeignKey("DeviceModelsId");

        // TwinChangeEvent has one Twin of type DigitalTwin
        modelBuilder.Entity<TwinChangeEvent>()
            .HasOne(x => x.Twin)
            .WithMany()
            .HasForeignKey("TwinId");


        // MaintenanceTicket has one Device of type IoTDevice
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // MaintenanceTicket has one Tenant of type Tenant
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // DataRetentionPolicy has one Tenant of type Tenant
        modelBuilder.Entity<DataRetentionPolicy>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // DataRetentionPolicy has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<DataRetentionPolicy>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("StreamsId");

        // SoftwareUpdateCampaign has one FirmwareRelease of type FirmwareRelease
        modelBuilder.Entity<SoftwareUpdateCampaign>()
            .HasOne(x => x.FirmwareRelease)
            .WithMany()
            .HasForeignKey("FirmwareReleaseId");

        // SoftwareUpdateCampaign has one DeviceGroup of type DeviceGroup
        modelBuilder.Entity<SoftwareUpdateCampaign>()
            .HasOne(x => x.DeviceGroup)
            .WithMany()
            .HasForeignKey("DeviceGroupId");


        // SoftwareUpdateCampaign has one or more Executions of type SoftwareUpdateExecution
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne<SoftwareUpdateCampaign>()
            .WithMany(parent => parent.Executions)
            .HasForeignKey("ExecutionsId");

        // SoftwareUpdateExecution has one Campaign of type SoftwareUpdateCampaign
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("CampaignId");

        // SoftwareUpdateExecution has one Device of type IoTDevice
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");


        // DeviceGroup has one Tenant of type Tenant
        modelBuilder.Entity<DeviceGroup>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");


        // DeviceGroup has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<DeviceGroup>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DevicesId");

        // UsageRecord has one Tenant of type Tenant
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("TenantId");

        // UsageRecord has one Device of type IoTDevice
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("DeviceId");

        // UsageRecord has one ConnectivityPlan of type ConnectivityPlan
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlanId");


    }
}
