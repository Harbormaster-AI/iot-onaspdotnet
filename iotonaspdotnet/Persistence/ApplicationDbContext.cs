                        using iotonaspdotnet.Domain.DeviceVendor;
                    using iotonaspdotnet.Domain.HardwareModule;
                    using iotonaspdotnet.Domain.DeviceModel;
                    using iotonaspdotnet.Domain.FirmwareRelease;
                    using iotonaspdotnet.Domain.IoTDevice;
                    using iotonaspdotnet.Domain.SensorInstance;
                    using iotonaspdotnet.Domain.ActuatorInstance;
                    using iotonaspdotnet.Domain.TelemetrySchema;
                    using iotonaspdotnet.Domain.TelemetryStream;
                    using iotonaspdotnet.Domain.CommandDefinition;
                    using iotonaspdotnet.Domain.CommandInvocation;
                    using iotonaspdotnet.Domain.AlertRule;
                    using iotonaspdotnet.Domain.Alert;
                    using iotonaspdotnet.Domain.Tenant;
                    using iotonaspdotnet.Domain.TenantUser;
                    using iotonaspdotnet.Domain.Site;
                    using iotonaspdotnet.Domain.Building;
                    using iotonaspdotnet.Domain.Floor;
                    using iotonaspdotnet.Domain.Room;
                    using iotonaspdotnet.Domain.Gateway;
                    using iotonaspdotnet.Domain.EdgeApplication;
                    using iotonaspdotnet.Domain.NetworkProfile;
                    using iotonaspdotnet.Domain.SimCard;
                    using iotonaspdotnet.Domain.ConnectivityPlan;
                    using iotonaspdotnet.Domain.MessagingEndpoint;
                    using iotonaspdotnet.Domain.AccessPolicy;
                    using iotonaspdotnet.Domain.ApiKey;
                    using iotonaspdotnet.Domain.DeviceCertificate;
                    using iotonaspdotnet.Domain.ProvisioningRecord;
                    using iotonaspdotnet.Domain.DigitalTwin;
                    using iotonaspdotnet.Domain.TwinTemplate;
                    using iotonaspdotnet.Domain.TwinChangeEvent;
                    using iotonaspdotnet.Domain.MaintenanceTicket;
                    using iotonaspdotnet.Domain.DataRetentionPolicy;
                    using iotonaspdotnet.Domain.SoftwareUpdateCampaign;
                    using iotonaspdotnet.Domain.SoftwareUpdateExecution;
                    using iotonaspdotnet.Domain.DeviceGroup;
                    using iotonaspdotnet.Domain.UsageRecord;
            
using Microsoft.EntityFrameworkCore;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
