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
            .HasForeignKey("DeviceVendor_Id");

        // DeviceVendor has one or more FirmwareReleases of type FirmwareRelease
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<DeviceVendor>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("DeviceVendor_Id");

        // DeviceVendor has one or more HardwareModules of type HardwareModule
        modelBuilder.Entity<HardwareModule>()
            .HasOne<DeviceVendor>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("DeviceVendor_Id");

        // HardwareModule has one Vendor of type DeviceVendor
        modelBuilder.Entity<HardwareModule>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("Vendor_Id");


        // DeviceModel has one Vendor of type DeviceVendor
        modelBuilder.Entity<DeviceModel>()
            .HasOne(x => x.Vendor)
            .WithMany()
            .HasForeignKey("Vendor_Id");

        // DeviceModel has one TwinTemplate of type TwinTemplate
        modelBuilder.Entity<DeviceModel>()
            .HasOne(x => x.TwinTemplate)
            .WithMany()
            .HasForeignKey("TwinTemplate_Id");


        // DeviceModel has one or more HardwareModules of type HardwareModule
        modelBuilder.Entity<HardwareModule>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.HardwareModules)
            .HasForeignKey("DeviceModel_Id");

        // DeviceModel has one or more FirmwareReleases of type FirmwareRelease
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.FirmwareReleases)
            .HasForeignKey("DeviceModel_Id");

        // DeviceModel has one or more CommandDefinitions of type CommandDefinition
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<DeviceModel>()
            .WithMany(parent => parent.CommandDefinitions)
            .HasForeignKey("DeviceModel_Id");

        // FirmwareRelease has one DeviceModel of type DeviceModel
        modelBuilder.Entity<FirmwareRelease>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModel_Id");


        // IoTDevice has one DeviceModel of type DeviceModel
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModel_Id");

        // IoTDevice has one Tenant of type Tenant
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");

        // IoTDevice has one Site of type Site
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("Site_Id");

        // IoTDevice has one Room of type Room
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("Room_Id");

        // IoTDevice has one Gateway of type Gateway
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("Gateway_Id");

        // IoTDevice has one DigitalTwin of type DigitalTwin
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwin_Id");

        // IoTDevice has one ProvisioningRecord of type ProvisioningRecord
        modelBuilder.Entity<IoTDevice>()
            .HasOne(x => x.ProvisioningRecord)
            .WithMany()
            .HasForeignKey("ProvisioningRecord_Id");


        // IoTDevice has one or more Sensors of type SensorInstance
        modelBuilder.Entity<SensorInstance>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Sensors)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more Actuators of type ActuatorInstance
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more Certificates of type DeviceCertificate
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more TelemetryStreams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more Alerts of type Alert
        modelBuilder.Entity<Alert>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more DeviceGroups of type DeviceGroup
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("IoTDevice_Id");

        // IoTDevice has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<IoTDevice>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("IoTDevice_Id");

        // SensorInstance has one Device of type IoTDevice
        modelBuilder.Entity<SensorInstance>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");


        // SensorInstance has one or more TelemetryStreams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<SensorInstance>()
            .WithMany(parent => parent.TelemetryStreams)
            .HasForeignKey("SensorInstance_Id");

        // ActuatorInstance has one Device of type IoTDevice
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");


        // ActuatorInstance has one or more SupportedCommands of type CommandDefinition
        modelBuilder.Entity<CommandDefinition>()
            .HasOne<ActuatorInstance>()
            .WithMany(parent => parent.SupportedCommands)
            .HasForeignKey("ActuatorInstance_Id");


        // TelemetrySchema has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<TelemetrySchema>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("TelemetrySchema_Id");

        // TelemetryStream has one Device of type IoTDevice
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // TelemetryStream has one Sensor of type SensorInstance
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Sensor)
            .WithMany()
            .HasForeignKey("Sensor_Id");

        // TelemetryStream has one Schema of type TelemetrySchema
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.Schema)
            .WithMany()
            .HasForeignKey("Schema_Id");

        // TelemetryStream has one MessagingEndpoint of type MessagingEndpoint
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.MessagingEndpoint)
            .WithMany()
            .HasForeignKey("MessagingEndpoint_Id");

        // TelemetryStream has one RetentionPolicy of type DataRetentionPolicy
        modelBuilder.Entity<TelemetryStream>()
            .HasOne(x => x.RetentionPolicy)
            .WithMany()
            .HasForeignKey("RetentionPolicy_Id");


        // CommandDefinition has one DeviceModel of type DeviceModel
        modelBuilder.Entity<CommandDefinition>()
            .HasOne(x => x.DeviceModel)
            .WithMany()
            .HasForeignKey("DeviceModel_Id");


        // CommandDefinition has one or more Actuators of type ActuatorInstance
        modelBuilder.Entity<ActuatorInstance>()
            .HasOne<CommandDefinition>()
            .WithMany(parent => parent.Actuators)
            .HasForeignKey("CommandDefinition_Id");

        // CommandDefinition has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<CommandDefinition>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("CommandDefinition_Id");

        // CommandInvocation has one Device of type IoTDevice
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // CommandInvocation has one CommandDefinition of type CommandDefinition
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.CommandDefinition)
            .WithMany()
            .HasForeignKey("CommandDefinition_Id");

        // CommandInvocation has one Actuator of type ActuatorInstance
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.Actuator)
            .WithMany()
            .HasForeignKey("Actuator_Id");

        // CommandInvocation has one User of type TenantUser
        modelBuilder.Entity<CommandInvocation>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey("User_Id");


        // AlertRule has one Tenant of type Tenant
        modelBuilder.Entity<AlertRule>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // AlertRule has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<AlertRule>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("AlertRule_Id");

        // AlertRule has one or more Alerts of type Alert
        modelBuilder.Entity<Alert>()
            .HasOne<AlertRule>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("AlertRule_Id");

        // Alert has one Device of type IoTDevice
        modelBuilder.Entity<Alert>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // Alert has one AlertRule of type AlertRule
        modelBuilder.Entity<Alert>()
            .HasOne(x => x.AlertRule)
            .WithMany()
            .HasForeignKey("AlertRule_Id");



        // Tenant has one or more Sites of type Site
        modelBuilder.Entity<Site>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Sites)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more Users of type TenantUser
        modelBuilder.Entity<TenantUser>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more DataRetentionPolicies of type DataRetentionPolicy
        modelBuilder.Entity<DataRetentionPolicy>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.DataRetentionPolicies)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more ConnectivityPlans of type ConnectivityPlan
        modelBuilder.Entity<ConnectivityPlan>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.ConnectivityPlans)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more SimCards of type SimCard
        modelBuilder.Entity<SimCard>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more MessagingEndpoints of type MessagingEndpoint
        modelBuilder.Entity<MessagingEndpoint>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.MessagingEndpoints)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more AccessPolicies of type AccessPolicy
        modelBuilder.Entity<AccessPolicy>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.AccessPolicies)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more DeviceGroups of type DeviceGroup
        modelBuilder.Entity<DeviceGroup>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.DeviceGroups)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more AlertRules of type AlertRule
        modelBuilder.Entity<AlertRule>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.AlertRules)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more MaintenanceTickets of type MaintenanceTicket
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.MaintenanceTickets)
            .HasForeignKey("Tenant_Id");

        // Tenant has one or more UsageRecords of type UsageRecord
        modelBuilder.Entity<UsageRecord>()
            .HasOne<Tenant>()
            .WithMany(parent => parent.UsageRecords)
            .HasForeignKey("Tenant_Id");

        // TenantUser has one Tenant of type Tenant
        modelBuilder.Entity<TenantUser>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // TenantUser has one or more CommandInvocations of type CommandInvocation
        modelBuilder.Entity<CommandInvocation>()
            .HasOne<TenantUser>()
            .WithMany(parent => parent.CommandInvocations)
            .HasForeignKey("TenantUser_Id");

        // Site has one Tenant of type Tenant
        modelBuilder.Entity<Site>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // Site has one or more Buildings of type Building
        modelBuilder.Entity<Building>()
            .HasOne<Site>()
            .WithMany(parent => parent.Buildings)
            .HasForeignKey("Site_Id");

        // Site has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Site>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("Site_Id");

        // Site has one or more Gateways of type Gateway
        modelBuilder.Entity<Gateway>()
            .HasOne<Site>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("Site_Id");

        // Building has one Site of type Site
        modelBuilder.Entity<Building>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("Site_Id");


        // Building has one or more Floors of type Floor
        modelBuilder.Entity<Floor>()
            .HasOne<Building>()
            .WithMany(parent => parent.Floors)
            .HasForeignKey("Building_Id");

        // Floor has one Building of type Building
        modelBuilder.Entity<Floor>()
            .HasOne(x => x.Building)
            .WithMany()
            .HasForeignKey("Building_Id");


        // Floor has one or more Rooms of type Room
        modelBuilder.Entity<Room>()
            .HasOne<Floor>()
            .WithMany(parent => parent.Rooms)
            .HasForeignKey("Floor_Id");

        // Room has one Floor of type Floor
        modelBuilder.Entity<Room>()
            .HasOne(x => x.Floor)
            .WithMany()
            .HasForeignKey("Floor_Id");


        // Room has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Room>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("Room_Id");

        // Room has one or more Gateways of type Gateway
        modelBuilder.Entity<Gateway>()
            .HasOne<Room>()
            .WithMany(parent => parent.Gateways)
            .HasForeignKey("Room_Id");

        // Gateway has one Site of type Site
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey("Site_Id");

        // Gateway has one Room of type Room
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey("Room_Id");

        // Gateway has one DigitalTwin of type DigitalTwin
        modelBuilder.Entity<Gateway>()
            .HasOne(x => x.DigitalTwin)
            .WithMany()
            .HasForeignKey("DigitalTwin_Id");


        // Gateway has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("Gateway_Id");

        // Gateway has one or more EdgeApplications of type EdgeApplication
        modelBuilder.Entity<EdgeApplication>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.EdgeApplications)
            .HasForeignKey("Gateway_Id");

        // Gateway has one or more Certificates of type DeviceCertificate
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.Certificates)
            .HasForeignKey("Gateway_Id");

        // Gateway has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<Gateway>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("Gateway_Id");

        // EdgeApplication has one Gateway of type Gateway
        modelBuilder.Entity<EdgeApplication>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("Gateway_Id");


        // NetworkProfile has one Device of type IoTDevice
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // NetworkProfile has one Gateway of type Gateway
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("Gateway_Id");

        // NetworkProfile has one SimCard of type SimCard
        modelBuilder.Entity<NetworkProfile>()
            .HasOne(x => x.SimCard)
            .WithMany()
            .HasForeignKey("SimCard_Id");


        // SimCard has one Tenant of type Tenant
        modelBuilder.Entity<SimCard>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");

        // SimCard has one ConnectivityPlan of type ConnectivityPlan
        modelBuilder.Entity<SimCard>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlan_Id");


        // SimCard has one or more NetworkProfiles of type NetworkProfile
        modelBuilder.Entity<NetworkProfile>()
            .HasOne<SimCard>()
            .WithMany(parent => parent.NetworkProfiles)
            .HasForeignKey("SimCard_Id");

        // ConnectivityPlan has one Tenant of type Tenant
        modelBuilder.Entity<ConnectivityPlan>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // ConnectivityPlan has one or more SimCards of type SimCard
        modelBuilder.Entity<SimCard>()
            .HasOne<ConnectivityPlan>()
            .WithMany(parent => parent.SimCards)
            .HasForeignKey("ConnectivityPlan_Id");

        // MessagingEndpoint has one Tenant of type Tenant
        modelBuilder.Entity<MessagingEndpoint>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // MessagingEndpoint has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<MessagingEndpoint>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("MessagingEndpoint_Id");

        // AccessPolicy has one Tenant of type Tenant
        modelBuilder.Entity<AccessPolicy>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // AccessPolicy has one or more ApiKeys of type ApiKey
        modelBuilder.Entity<ApiKey>()
            .HasOne<AccessPolicy>()
            .WithMany(parent => parent.ApiKeys)
            .HasForeignKey("AccessPolicy_Id");

        // AccessPolicy has one or more Users of type TenantUser
        modelBuilder.Entity<TenantUser>()
            .HasOne<AccessPolicy>()
            .WithMany(parent => parent.Users)
            .HasForeignKey("AccessPolicy_Id");

        // ApiKey has one AccessPolicy of type AccessPolicy
        modelBuilder.Entity<ApiKey>()
            .HasOne(x => x.AccessPolicy)
            .WithMany()
            .HasForeignKey("AccessPolicy_Id");


        // DeviceCertificate has one Device of type IoTDevice
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // DeviceCertificate has one Gateway of type Gateway
        modelBuilder.Entity<DeviceCertificate>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("Gateway_Id");


        // ProvisioningRecord has one Device of type IoTDevice
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // ProvisioningRecord has one Certificate of type DeviceCertificate
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Certificate)
            .WithMany()
            .HasForeignKey("Certificate_Id");

        // ProvisioningRecord has one Tenant of type Tenant
        modelBuilder.Entity<ProvisioningRecord>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // DigitalTwin has one Device of type IoTDevice
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // DigitalTwin has one Gateway of type Gateway
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Gateway)
            .WithMany()
            .HasForeignKey("Gateway_Id");

        // DigitalTwin has one Template of type TwinTemplate
        modelBuilder.Entity<DigitalTwin>()
            .HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey("Template_Id");


        // DigitalTwin has one or more ChangeEvents of type TwinChangeEvent
        modelBuilder.Entity<TwinChangeEvent>()
            .HasOne<DigitalTwin>()
            .WithMany(parent => parent.ChangeEvents)
            .HasForeignKey("DigitalTwin_Id");


        // TwinTemplate has one or more DeviceModels of type DeviceModel
        modelBuilder.Entity<DeviceModel>()
            .HasOne<TwinTemplate>()
            .WithMany(parent => parent.DeviceModels)
            .HasForeignKey("TwinTemplate_Id");

        // TwinChangeEvent has one Twin of type DigitalTwin
        modelBuilder.Entity<TwinChangeEvent>()
            .HasOne(x => x.Twin)
            .WithMany()
            .HasForeignKey("Twin_Id");


        // MaintenanceTicket has one Device of type IoTDevice
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // MaintenanceTicket has one Tenant of type Tenant
        modelBuilder.Entity<MaintenanceTicket>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // DataRetentionPolicy has one Tenant of type Tenant
        modelBuilder.Entity<DataRetentionPolicy>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // DataRetentionPolicy has one or more Streams of type TelemetryStream
        modelBuilder.Entity<TelemetryStream>()
            .HasOne<DataRetentionPolicy>()
            .WithMany(parent => parent.Streams)
            .HasForeignKey("DataRetentionPolicy_Id");

        // SoftwareUpdateCampaign has one FirmwareRelease of type FirmwareRelease
        modelBuilder.Entity<SoftwareUpdateCampaign>()
            .HasOne(x => x.FirmwareRelease)
            .WithMany()
            .HasForeignKey("FirmwareRelease_Id");

        // SoftwareUpdateCampaign has one DeviceGroup of type DeviceGroup
        modelBuilder.Entity<SoftwareUpdateCampaign>()
            .HasOne(x => x.DeviceGroup)
            .WithMany()
            .HasForeignKey("DeviceGroup_Id");


        // SoftwareUpdateCampaign has one or more Executions of type SoftwareUpdateExecution
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne<SoftwareUpdateCampaign>()
            .WithMany(parent => parent.Executions)
            .HasForeignKey("SoftwareUpdateCampaign_Id");

        // SoftwareUpdateExecution has one Campaign of type SoftwareUpdateCampaign
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne(x => x.Campaign)
            .WithMany()
            .HasForeignKey("Campaign_Id");

        // SoftwareUpdateExecution has one Device of type IoTDevice
        modelBuilder.Entity<SoftwareUpdateExecution>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");


        // DeviceGroup has one Tenant of type Tenant
        modelBuilder.Entity<DeviceGroup>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");


        // DeviceGroup has one or more Devices of type IoTDevice
        modelBuilder.Entity<IoTDevice>()
            .HasOne<DeviceGroup>()
            .WithMany(parent => parent.Devices)
            .HasForeignKey("DeviceGroup_Id");

        // UsageRecord has one Tenant of type Tenant
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey("Tenant_Id");

        // UsageRecord has one Device of type IoTDevice
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey("Device_Id");

        // UsageRecord has one ConnectivityPlan of type ConnectivityPlan
        modelBuilder.Entity<UsageRecord>()
            .HasOne(x => x.ConnectivityPlan)
            .WithMany()
            .HasForeignKey("ConnectivityPlan_Id");


    }
}
