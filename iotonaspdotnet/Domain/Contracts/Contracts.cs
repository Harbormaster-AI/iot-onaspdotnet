using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class DeviceVendorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class DeviceVendorResponse : DeviceVendorRequest {
    public static DeviceVendorResponse FromModel(DeviceVendor model) {
        return new DeviceVendorResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            HeadquartersCountry = model.HeadquartersCountry,
            Website = model.Website,
        };
    }
}

public class HardwareModuleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ModuleCode { get; set; } 
 public virtual Uri_? DatasheetUri { get; set; } 
 public virtual ModuleType? ModuleType { get; set; } 
}

public class HardwareModuleResponse : HardwareModuleRequest {
    public static HardwareModuleResponse FromModel(HardwareModule model) {
        return new HardwareModuleResponse {
            Id = model.Id,
            ModuleCode = model.ModuleCode,
            DatasheetUri = model.DatasheetUri,
            ModuleType = model.ModuleType,
        };
    }
}

public class DeviceModelRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ModelNumber { get; set; } 
 public virtual string? HardwareRevision { get; set; } 
 public virtual ConnectivityType? SupportedConnectivity { get; set; } 
 public virtual TelemetryEncoding? DefaultTelemetryEncoding { get; set; } 
}

public class DeviceModelResponse : DeviceModelRequest {
    public static DeviceModelResponse FromModel(DeviceModel model) {
        return new DeviceModelResponse {
            Id = model.Id,
            Name = model.Name,
            ModelNumber = model.ModelNumber,
            HardwareRevision = model.HardwareRevision,
            SupportedConnectivity = model.SupportedConnectivity,
            DefaultTelemetryEncoding = model.DefaultTelemetryEncoding,
        };
    }
}

public class FirmwareReleaseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual FirmwareVersion? Version { get; set; } 
 public virtual DateOnly? ReleaseDate { get; set; } 
 public virtual string? ReleaseNotes { get; set; } 
 public virtual Checksum? Checksum { get; set; } 
}

public class FirmwareReleaseResponse : FirmwareReleaseRequest {
    public static FirmwareReleaseResponse FromModel(FirmwareRelease model) {
        return new FirmwareReleaseResponse {
            Id = model.Id,
            Version = model.Version,
            ReleaseDate = model.ReleaseDate,
            ReleaseNotes = model.ReleaseNotes,
            Checksum = model.Checksum,
        };
    }
}

public class IoTDeviceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DeviceId? DeviceId { get; set; } 
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime_? LastSeen { get; set; } 
 public virtual FirmwareVersion? FirmwareVersion { get; set; } 
 public virtual DeviceStatus? Status { get; set; } 
 public virtual PowerSource? PowerSource { get; set; } 
}

public class IoTDeviceResponse : IoTDeviceRequest {
    public static IoTDeviceResponse FromModel(IoTDevice model) {
        return new IoTDeviceResponse {
            Id = model.Id,
            DeviceId = model.DeviceId,
            SerialNumber = model.SerialNumber,
            LastSeen = model.LastSeen,
            FirmwareVersion = model.FirmwareVersion,
            Status = model.Status,
            PowerSource = model.PowerSource,
        };
    }
}

public class SensorInstanceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Unit { get; set; } 
 public virtual int? SamplingIntervalMs { get; set; } 
 public virtual SensorType? SensorType { get; set; } 
}

public class SensorInstanceResponse : SensorInstanceRequest {
    public static SensorInstanceResponse FromModel(SensorInstance model) {
        return new SensorInstanceResponse {
            Id = model.Id,
            Name = model.Name,
            Unit = model.Unit,
            SamplingIntervalMs = model.SamplingIntervalMs,
            SensorType = model.SensorType,
        };
    }
}

public class ActuatorInstanceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual TopicName? CommandTopic { get; set; } 
 public virtual ActuatorType? ActuatorType { get; set; } 
}

public class ActuatorInstanceResponse : ActuatorInstanceRequest {
    public static ActuatorInstanceResponse FromModel(ActuatorInstance model) {
        return new ActuatorInstanceResponse {
            Id = model.Id,
            Name = model.Name,
            CommandTopic = model.CommandTopic,
            ActuatorType = model.ActuatorType,
        };
    }
}

public class TelemetrySchemaRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SchemaId { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
 public virtual TelemetryEncoding? Encoding { get; set; } 
}

public class TelemetrySchemaResponse : TelemetrySchemaRequest {
    public static TelemetrySchemaResponse FromModel(TelemetrySchema model) {
        return new TelemetrySchemaResponse {
            Id = model.Id,
            SchemaId = model.SchemaId,
            SchemaUri = model.SchemaUri,
            Encoding = model.Encoding,
        };
    }
}

public class TelemetryStreamRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? StreamName { get; set; } 
 public virtual int? RetentionDays { get; set; } 
 public virtual MessageQoS? Qos { get; set; } 
}

public class TelemetryStreamResponse : TelemetryStreamRequest {
    public static TelemetryStreamResponse FromModel(TelemetryStream model) {
        return new TelemetryStreamResponse {
            Id = model.Id,
            StreamName = model.StreamName,
            RetentionDays = model.RetentionDays,
            Qos = model.Qos,
        };
    }
}

public class CommandDefinitionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Uri_? RequestSchemaUri { get; set; } 
 public virtual Uri_? ResponseSchemaUri { get; set; } 
 public virtual int? TimeoutSeconds { get; set; } 
}

public class CommandDefinitionResponse : CommandDefinitionRequest {
    public static CommandDefinitionResponse FromModel(CommandDefinition model) {
        return new CommandDefinitionResponse {
            Id = model.Id,
            Name = model.Name,
            RequestSchemaUri = model.RequestSchemaUri,
            ResponseSchemaUri = model.ResponseSchemaUri,
            TimeoutSeconds = model.TimeoutSeconds,
        };
    }
}

public class CommandInvocationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? InvocationId { get; set; } 
 public virtual DateTime_? RequestedAt { get; set; } 
 public virtual DateTime_? CompletedAt { get; set; } 
 public virtual CommandStatus? Status { get; set; } 
}

public class CommandInvocationResponse : CommandInvocationRequest {
    public static CommandInvocationResponse FromModel(CommandInvocation model) {
        return new CommandInvocationResponse {
            Id = model.Id,
            InvocationId = model.InvocationId,
            RequestedAt = model.RequestedAt,
            CompletedAt = model.CompletedAt,
            Status = model.Status,
        };
    }
}

public class AlertRuleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Expression { get; set; } 
 public virtual AlertSeverity? Severity { get; set; } 
}

public class AlertRuleResponse : AlertRuleRequest {
    public static AlertRuleResponse FromModel(AlertRule model) {
        return new AlertRuleResponse {
            Id = model.Id,
            Name = model.Name,
            Expression = model.Expression,
            Severity = model.Severity,
        };
    }
}

public class AlertRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime_? RaisedAt { get; set; } 
 public virtual DateTime_? ClearedAt { get; set; } 
 public virtual string? Message { get; set; } 
 public virtual AlertStatus? Status { get; set; } 
}

public class AlertResponse : AlertRequest {
    public static AlertResponse FromModel(Alert model) {
        return new AlertResponse {
            Id = model.Id,
            RaisedAt = model.RaisedAt,
            ClearedAt = model.ClearedAt,
            Message = model.Message,
            Status = model.Status,
        };
    }
}

public class TenantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual TenantType? TenantType { get; set; } 
}

public class TenantResponse : TenantRequest {
    public static TenantResponse FromModel(Tenant model) {
        return new TenantResponse {
            Id = model.Id,
            Name = model.Name,
            TenantType = model.TenantType,
        };
    }
}

public class TenantUserRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? Email { get; set; } 
 public virtual UserRole? Role { get; set; } 
}

public class TenantUserResponse : TenantUserRequest {
    public static TenantUserResponse FromModel(TenantUser model) {
        return new TenantUserResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = model.Role,
        };
    }
}

public class SiteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
 public virtual decimal? Latitude { get; set; } 
 public virtual decimal? Longitude { get; set; } 
}

public class SiteResponse : SiteRequest {
    public static SiteResponse FromModel(Site model) {
        return new SiteResponse {
            Id = model.Id,
            Name = model.Name,
            Address = model.Address,
            Timezone = model.Timezone,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
        };
    }
}

public class BuildingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class BuildingResponse : BuildingRequest {
    public static BuildingResponse FromModel(Building model) {
        return new BuildingResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class FloorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual int? Level { get; set; } 
}

public class FloorResponse : FloorRequest {
    public static FloorResponse FromModel(Floor model) {
        return new FloorResponse {
            Id = model.Id,
            Name = model.Name,
            Level = model.Level,
        };
    }
}

public class RoomRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
}

public class RoomResponse : RoomRequest {
    public static RoomResponse FromModel(Room model) {
        return new RoomResponse {
            Id = model.Id,
            Name = model.Name,
        };
    }
}

public class GatewayRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SoftwareVersion { get; set; } 
 public virtual DeviceStatus? Status { get; set; } 
}

public class GatewayResponse : GatewayRequest {
    public static GatewayResponse FromModel(Gateway model) {
        return new GatewayResponse {
            Id = model.Id,
            SoftwareVersion = model.SoftwareVersion,
            Status = model.Status,
        };
    }
}

public class EdgeApplicationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Version { get; set; } 
 public virtual string? Image { get; set; } 
 public virtual DeploymentStatus? Status { get; set; } 
}

public class EdgeApplicationResponse : EdgeApplicationRequest {
    public static EdgeApplicationResponse FromModel(EdgeApplication model) {
        return new EdgeApplicationResponse {
            Id = model.Id,
            Name = model.Name,
            Version = model.Version,
            Image = model.Image,
            Status = model.Status,
        };
    }
}

public class NetworkProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ProfileName { get; set; } 
 public virtual string? Ssid { get; set; } 
 public virtual string? Apn { get; set; } 
 public virtual ConnectivityType? ConnectivityType { get; set; } 
}

public class NetworkProfileResponse : NetworkProfileRequest {
    public static NetworkProfileResponse FromModel(NetworkProfile model) {
        return new NetworkProfileResponse {
            Id = model.Id,
            ProfileName = model.ProfileName,
            Ssid = model.Ssid,
            Apn = model.Apn,
            ConnectivityType = model.ConnectivityType,
        };
    }
}

public class SimCardRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Iccid { get; set; } 
 public virtual string? Imsi { get; set; } 
 public virtual string? Carrier { get; set; } 
 public virtual SimStatus? Status { get; set; } 
}

public class SimCardResponse : SimCardRequest {
    public static SimCardResponse FromModel(SimCard model) {
        return new SimCardResponse {
            Id = model.Id,
            Iccid = model.Iccid,
            Imsi = model.Imsi,
            Carrier = model.Carrier,
            Status = model.Status,
        };
    }
}

public class ConnectivityPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual int? DataCapMB { get; set; } 
 public virtual int? BillingCycleDays { get; set; } 
}

public class ConnectivityPlanResponse : ConnectivityPlanRequest {
    public static ConnectivityPlanResponse FromModel(ConnectivityPlan model) {
        return new ConnectivityPlanResponse {
            Id = model.Id,
            Name = model.Name,
            DataCapMB = model.DataCapMB,
            BillingCycleDays = model.BillingCycleDays,
        };
    }
}

public class MessagingEndpointRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Host { get; set; } 
 public virtual int? Port { get; set; } 
 public virtual bool? Secure { get; set; } 
 public virtual MessagingProtocol? Protocol { get; set; } 
}

public class MessagingEndpointResponse : MessagingEndpointRequest {
    public static MessagingEndpointResponse FromModel(MessagingEndpoint model) {
        return new MessagingEndpointResponse {
            Id = model.Id,
            Host = model.Host,
            Port = model.Port,
            Secure = model.Secure,
            Protocol = model.Protocol,
        };
    }
}

public class AccessPolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Scope { get; set; } 
 public virtual DateTime_? ExpiresAt { get; set; } 
}

public class AccessPolicyResponse : AccessPolicyRequest {
    public static AccessPolicyResponse FromModel(AccessPolicy model) {
        return new AccessPolicyResponse {
            Id = model.Id,
            Name = model.Name,
            Scope = model.Scope,
            ExpiresAt = model.ExpiresAt,
        };
    }
}

public class ApiKeyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? KeyId { get; set; } 
 public virtual string? HashedSecret { get; set; } 
 public virtual DateTime_? CreatedAt { get; set; } 
 public virtual DateTime_? LastUsedAt { get; set; } 
}

public class ApiKeyResponse : ApiKeyRequest {
    public static ApiKeyResponse FromModel(ApiKey model) {
        return new ApiKeyResponse {
            Id = model.Id,
            KeyId = model.KeyId,
            HashedSecret = model.HashedSecret,
            CreatedAt = model.CreatedAt,
            LastUsedAt = model.LastUsedAt,
        };
    }
}

public class DeviceCertificateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime_? NotBefore { get; set; } 
 public virtual DateTime_? NotAfter { get; set; } 
 public virtual string? Fingerprint { get; set; } 
 public virtual CertificateType? CertificateType { get; set; } 
}

public class DeviceCertificateResponse : DeviceCertificateRequest {
    public static DeviceCertificateResponse FromModel(DeviceCertificate model) {
        return new DeviceCertificateResponse {
            Id = model.Id,
            SerialNumber = model.SerialNumber,
            NotBefore = model.NotBefore,
            NotAfter = model.NotAfter,
            Fingerprint = model.Fingerprint,
            CertificateType = model.CertificateType,
        };
    }
}

public class ProvisioningRecordRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime_? EnrolledAt { get; set; } 
 public virtual string? ProvisioningService { get; set; } 
 public virtual ProvisioningMethod? Method { get; set; } 
 public virtual ProvisioningStatus? Status { get; set; } 
}

public class ProvisioningRecordResponse : ProvisioningRecordRequest {
    public static ProvisioningRecordResponse FromModel(ProvisioningRecord model) {
        return new ProvisioningRecordResponse {
            Id = model.Id,
            EnrolledAt = model.EnrolledAt,
            ProvisioningService = model.ProvisioningService,
            Method = model.Method,
            Status = model.Status,
        };
    }
}

public class DigitalTwinRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TwinId { get; set; } 
 public virtual int? DesiredStateVersion { get; set; } 
 public virtual int? ReportedStateVersion { get; set; } 
 public virtual DateTime_? LastSyncAt { get; set; } 
}

public class DigitalTwinResponse : DigitalTwinRequest {
    public static DigitalTwinResponse FromModel(DigitalTwin model) {
        return new DigitalTwinResponse {
            Id = model.Id,
            TwinId = model.TwinId,
            DesiredStateVersion = model.DesiredStateVersion,
            ReportedStateVersion = model.ReportedStateVersion,
            LastSyncAt = model.LastSyncAt,
        };
    }
}

public class TwinTemplateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
 public virtual string? Version { get; set; } 
}

public class TwinTemplateResponse : TwinTemplateRequest {
    public static TwinTemplateResponse FromModel(TwinTemplate model) {
        return new TwinTemplateResponse {
            Id = model.Id,
            Name = model.Name,
            SchemaUri = model.SchemaUri,
            Version = model.Version,
        };
    }
}

public class TwinChangeEventRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EventId { get; set; } 
 public virtual DateTime_? OccurredAt { get; set; } 
 public virtual TwinChangeType? ChangeType { get; set; } 
}

public class TwinChangeEventResponse : TwinChangeEventRequest {
    public static TwinChangeEventResponse FromModel(TwinChangeEvent model) {
        return new TwinChangeEventResponse {
            Id = model.Id,
            EventId = model.EventId,
            OccurredAt = model.OccurredAt,
            ChangeType = model.ChangeType,
        };
    }
}

public class MaintenanceTicketRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TicketNumber { get; set; } 
 public virtual DateTime_? OpenedAt { get; set; } 
 public virtual DateTime_? ClosedAt { get; set; } 
 public virtual MaintenancePriority? Priority { get; set; } 
 public virtual MaintenanceStatus? Status { get; set; } 
}

public class MaintenanceTicketResponse : MaintenanceTicketRequest {
    public static MaintenanceTicketResponse FromModel(MaintenanceTicket model) {
        return new MaintenanceTicketResponse {
            Id = model.Id,
            TicketNumber = model.TicketNumber,
            OpenedAt = model.OpenedAt,
            ClosedAt = model.ClosedAt,
            Priority = model.Priority,
            Status = model.Status,
        };
    }
}

public class DataRetentionPolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual int? RetentionDays { get; set; } 
}

public class DataRetentionPolicyResponse : DataRetentionPolicyRequest {
    public static DataRetentionPolicyResponse FromModel(DataRetentionPolicy model) {
        return new DataRetentionPolicyResponse {
            Id = model.Id,
            Name = model.Name,
            RetentionDays = model.RetentionDays,
        };
    }
}

public class SoftwareUpdateCampaignRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CampaignCode { get; set; } 
 public virtual DateTime_? ScheduledStart { get; set; } 
 public virtual DateTime_? ScheduledEnd { get; set; } 
 public virtual UpdateCampaignStatus? Status { get; set; } 
}

public class SoftwareUpdateCampaignResponse : SoftwareUpdateCampaignRequest {
    public static SoftwareUpdateCampaignResponse FromModel(SoftwareUpdateCampaign model) {
        return new SoftwareUpdateCampaignResponse {
            Id = model.Id,
            CampaignCode = model.CampaignCode,
            ScheduledStart = model.ScheduledStart,
            ScheduledEnd = model.ScheduledEnd,
            Status = model.Status,
        };
    }
}

public class SoftwareUpdateExecutionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime_? StartedAt { get; set; } 
 public virtual DateTime_? CompletedAt { get; set; } 
 public virtual UpdateStatus? Status { get; set; } 
}

public class SoftwareUpdateExecutionResponse : SoftwareUpdateExecutionRequest {
    public static SoftwareUpdateExecutionResponse FromModel(SoftwareUpdateExecution model) {
        return new SoftwareUpdateExecutionResponse {
            Id = model.Id,
            StartedAt = model.StartedAt,
            CompletedAt = model.CompletedAt,
            Status = model.Status,
        };
    }
}

public class DeviceGroupRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Criteria { get; set; } 
}

public class DeviceGroupResponse : DeviceGroupRequest {
    public static DeviceGroupResponse FromModel(DeviceGroup model) {
        return new DeviceGroupResponse {
            Id = model.Id,
            Name = model.Name,
            Criteria = model.Criteria,
        };
    }
}

public class UsageRecordRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual int? MessagesSent { get; set; } 
 public virtual int? DataVolumeMB { get; set; } 
}

public class UsageRecordResponse : UsageRecordRequest {
    public static UsageRecordResponse FromModel(UsageRecord model) {
        return new UsageRecordResponse {
            Id = model.Id,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            MessagesSent = model.MessagesSent,
            DataVolumeMB = model.DataVolumeMB,
        };
    }
}

