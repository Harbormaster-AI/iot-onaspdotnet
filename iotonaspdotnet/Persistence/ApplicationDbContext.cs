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


    }
}
