using System.ComponentModel.DataAnnotations.Schema;
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
 public virtual long? DevicevendorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class DeviceVendorResponse : DeviceVendorRequest {
    public static DeviceVendorResponse FromModel(DeviceVendor model) {
        return new DeviceVendorResponse {
            Id = model.Id,
            DevicevendorId = model.DevicevendorId,
            Name = model.Name,
            LegalName = model.LegalName,
            HeadquartersCountry = model.HeadquartersCountry,
            Website = model.Website,
            DeviceModels = model.DeviceModels,
            FirmwareReleases = model.FirmwareReleases,
            HardwareModules = model.HardwareModules,
        };
    }
}


public class HardwareModuleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? HardwaremoduleId { get; set; } 
 public virtual string? ModuleCode { get; set; } 
 public virtual Uri_? DatasheetUri { get; set; } 
 public virtual ModuleType? ModuleType { get; set; } 
}

public class HardwareModuleResponse : HardwareModuleRequest {
    public static HardwareModuleResponse FromModel(HardwareModule model) {
        return new HardwareModuleResponse {
            Id = model.Id,
            HardwaremoduleId = model.HardwaremoduleId,
            ModuleCode = model.ModuleCode,
            DatasheetUri = model.DatasheetUri,
            Vendor = model.Vendor,
            ModuleType = model.ModuleType,
        };
    }
}


public class DeviceModelRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? DevicemodelId { get; set; } 
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
            DevicemodelId = model.DevicemodelId,
            Name = model.Name,
            ModelNumber = model.ModelNumber,
            HardwareRevision = model.HardwareRevision,
            Vendor = model.Vendor,
            HardwareModules = model.HardwareModules,
            TwinTemplate = model.TwinTemplate,
            FirmwareReleases = model.FirmwareReleases,
            CommandDefinitions = model.CommandDefinitions,
            SupportedConnectivity = model.SupportedConnectivity,
            DefaultTelemetryEncoding = model.DefaultTelemetryEncoding,
        };
    }
}


public class FirmwareReleaseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? FirmwarereleaseId { get; set; } 
 public virtual FirmwareVersion? Version { get; set; } 
 public virtual DateOnly? ReleaseDate { get; set; } 
 public virtual string? ReleaseNotes { get; set; } 
 public virtual Checksum? Checksum { get; set; } 
}

public class FirmwareReleaseResponse : FirmwareReleaseRequest {
    public static FirmwareReleaseResponse FromModel(FirmwareRelease model) {
        return new FirmwareReleaseResponse {
            Id = model.Id,
            FirmwarereleaseId = model.FirmwarereleaseId,
            Version = model.Version,
            ReleaseDate = model.ReleaseDate,
            ReleaseNotes = model.ReleaseNotes,
            Checksum = model.Checksum,
            DeviceModel = model.DeviceModel,
        };
    }
}


public class IoTDeviceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? IotdeviceId { get; set; } 
 public virtual DeviceId? DeviceId { get; set; } 
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime? LastSeen { get; set; } 
 public virtual FirmwareVersion? FirmwareVersion { get; set; } 
 public virtual DeviceStatus? Status { get; set; } 
 public virtual PowerSource? PowerSource { get; set; } 
}

public class IoTDeviceResponse : IoTDeviceRequest {
    public static IoTDeviceResponse FromModel(IoTDevice model) {
        return new IoTDeviceResponse {
            Id = model.Id,
            IotdeviceId = model.IotdeviceId,
            DeviceId = model.DeviceId,
            SerialNumber = model.SerialNumber,
            LastSeen = model.LastSeen,
            FirmwareVersion = model.FirmwareVersion,
            DeviceModel = model.DeviceModel,
            Tenant = model.Tenant,
            Site = model.Site,
            Room = model.Room,
            Gateway = model.Gateway,
            Sensors = model.Sensors,
            Actuators = model.Actuators,
            Certificates = model.Certificates,
            DigitalTwin = model.DigitalTwin,
            TelemetryStreams = model.TelemetryStreams,
            CommandInvocations = model.CommandInvocations,
            Alerts = model.Alerts,
            ProvisioningRecord = model.ProvisioningRecord,
            DeviceGroups = model.DeviceGroups,
            NetworkProfiles = model.NetworkProfiles,
            Status = model.Status,
            PowerSource = model.PowerSource,
        };
    }
}


public class SensorInstanceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? SensorinstanceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Unit { get; set; } 
 public virtual int? SamplingIntervalMs { get; set; } 
 public virtual SensorType? SensorType { get; set; } 
}

public class SensorInstanceResponse : SensorInstanceRequest {
    public static SensorInstanceResponse FromModel(SensorInstance model) {
        return new SensorInstanceResponse {
            Id = model.Id,
            SensorinstanceId = model.SensorinstanceId,
            Name = model.Name,
            Unit = model.Unit,
            SamplingIntervalMs = model.SamplingIntervalMs,
            Device = model.Device,
            TelemetryStreams = model.TelemetryStreams,
            SensorType = model.SensorType,
        };
    }
}


public class ActuatorInstanceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? ActuatorinstanceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual TopicName? CommandTopic { get; set; } 
 public virtual ActuatorType? ActuatorType { get; set; } 
}

public class ActuatorInstanceResponse : ActuatorInstanceRequest {
    public static ActuatorInstanceResponse FromModel(ActuatorInstance model) {
        return new ActuatorInstanceResponse {
            Id = model.Id,
            ActuatorinstanceId = model.ActuatorinstanceId,
            Name = model.Name,
            CommandTopic = model.CommandTopic,
            Device = model.Device,
            SupportedCommands = model.SupportedCommands,
            ActuatorType = model.ActuatorType,
        };
    }
}


public class TelemetrySchemaRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TelemetryschemaId { get; set; } 
 public virtual string? SchemaId { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
 public virtual TelemetryEncoding? Encoding { get; set; } 
}

public class TelemetrySchemaResponse : TelemetrySchemaRequest {
    public static TelemetrySchemaResponse FromModel(TelemetrySchema model) {
        return new TelemetrySchemaResponse {
            Id = model.Id,
            TelemetryschemaId = model.TelemetryschemaId,
            SchemaId = model.SchemaId,
            SchemaUri = model.SchemaUri,
            Streams = model.Streams,
            Encoding = model.Encoding,
        };
    }
}


public class TelemetryStreamRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TelemetrystreamId { get; set; } 
 public virtual string? StreamName { get; set; } 
 public virtual int? RetentionDays { get; set; } 
 public virtual MessageQoS? Qos { get; set; } 
}

public class TelemetryStreamResponse : TelemetryStreamRequest {
    public static TelemetryStreamResponse FromModel(TelemetryStream model) {
        return new TelemetryStreamResponse {
            Id = model.Id,
            TelemetrystreamId = model.TelemetrystreamId,
            StreamName = model.StreamName,
            RetentionDays = model.RetentionDays,
            Device = model.Device,
            Sensor = model.Sensor,
            Schema = model.Schema,
            MessagingEndpoint = model.MessagingEndpoint,
            RetentionPolicy = model.RetentionPolicy,
            Qos = model.Qos,
        };
    }
}


public class CommandDefinitionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? CommanddefinitionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Uri_? RequestSchemaUri { get; set; } 
 public virtual Uri_? ResponseSchemaUri { get; set; } 
 public virtual int? TimeoutSeconds { get; set; } 
}

public class CommandDefinitionResponse : CommandDefinitionRequest {
    public static CommandDefinitionResponse FromModel(CommandDefinition model) {
        return new CommandDefinitionResponse {
            Id = model.Id,
            CommanddefinitionId = model.CommanddefinitionId,
            Name = model.Name,
            RequestSchemaUri = model.RequestSchemaUri,
            ResponseSchemaUri = model.ResponseSchemaUri,
            TimeoutSeconds = model.TimeoutSeconds,
            DeviceModel = model.DeviceModel,
            Actuators = model.Actuators,
            CommandInvocations = model.CommandInvocations,
        };
    }
}


public class CommandInvocationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? CommandinvocationId { get; set; } 
 public virtual string? InvocationId { get; set; } 
 public virtual DateTime? RequestedAt { get; set; } 
 public virtual DateTime? CompletedAt { get; set; } 
 public virtual CommandStatus? Status { get; set; } 
}

public class CommandInvocationResponse : CommandInvocationRequest {
    public static CommandInvocationResponse FromModel(CommandInvocation model) {
        return new CommandInvocationResponse {
            Id = model.Id,
            CommandinvocationId = model.CommandinvocationId,
            InvocationId = model.InvocationId,
            RequestedAt = model.RequestedAt,
            CompletedAt = model.CompletedAt,
            Device = model.Device,
            CommandDefinition = model.CommandDefinition,
            Actuator = model.Actuator,
            User = model.User,
            Status = model.Status,
        };
    }
}


public class AlertRuleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? AlertruleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Expression { get; set; } 
 public virtual AlertSeverity? Severity { get; set; } 
}

public class AlertRuleResponse : AlertRuleRequest {
    public static AlertRuleResponse FromModel(AlertRule model) {
        return new AlertRuleResponse {
            Id = model.Id,
            AlertruleId = model.AlertruleId,
            Name = model.Name,
            Expression = model.Expression,
            Tenant = model.Tenant,
            Streams = model.Streams,
            Alerts = model.Alerts,
            Severity = model.Severity,
        };
    }
}


public class AlertRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? AlertId { get; set; } 
 public virtual DateTime? RaisedAt { get; set; } 
 public virtual DateTime? ClearedAt { get; set; } 
 public virtual string? Message { get; set; } 
 public virtual AlertStatus? Status { get; set; } 
}

public class AlertResponse : AlertRequest {
    public static AlertResponse FromModel(Alert model) {
        return new AlertResponse {
            Id = model.Id,
            AlertId = model.AlertId,
            RaisedAt = model.RaisedAt,
            ClearedAt = model.ClearedAt,
            Message = model.Message,
            Device = model.Device,
            AlertRule = model.AlertRule,
            Status = model.Status,
        };
    }
}


public class TenantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TenantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual TenantType? TenantType { get; set; } 
}

public class TenantResponse : TenantRequest {
    public static TenantResponse FromModel(Tenant model) {
        return new TenantResponse {
            Id = model.Id,
            TenantId = model.TenantId,
            Name = model.Name,
            Sites = model.Sites,
            Users = model.Users,
            Devices = model.Devices,
            DataRetentionPolicies = model.DataRetentionPolicies,
            ConnectivityPlans = model.ConnectivityPlans,
            SimCards = model.SimCards,
            MessagingEndpoints = model.MessagingEndpoints,
            AccessPolicies = model.AccessPolicies,
            DeviceGroups = model.DeviceGroups,
            AlertRules = model.AlertRules,
            MaintenanceTickets = model.MaintenanceTickets,
            UsageRecords = model.UsageRecords,
            TenantType = model.TenantType,
        };
    }
}


public class TenantUserRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TenantuserId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? Email { get; set; } 
 public virtual UserRole? Role { get; set; } 
}

public class TenantUserResponse : TenantUserRequest {
    public static TenantUserResponse FromModel(TenantUser model) {
        return new TenantUserResponse {
            Id = model.Id,
            TenantuserId = model.TenantuserId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Tenant = model.Tenant,
            CommandInvocations = model.CommandInvocations,
            Role = model.Role,
        };
    }
}


public class SiteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? SiteId { get; set; } 
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
            SiteId = model.SiteId,
            Name = model.Name,
            Address = model.Address,
            Timezone = model.Timezone,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Tenant = model.Tenant,
            Buildings = model.Buildings,
            Devices = model.Devices,
            Gateways = model.Gateways,
        };
    }
}


public class BuildingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? BuildingId { get; set; } 
 public virtual string? Name { get; set; } 
}

public class BuildingResponse : BuildingRequest {
    public static BuildingResponse FromModel(Building model) {
        return new BuildingResponse {
            Id = model.Id,
            BuildingId = model.BuildingId,
            Name = model.Name,
            Site = model.Site,
            Floors = model.Floors,
        };
    }
}


public class FloorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? FloorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? Level { get; set; } 
}

public class FloorResponse : FloorRequest {
    public static FloorResponse FromModel(Floor model) {
        return new FloorResponse {
            Id = model.Id,
            FloorId = model.FloorId,
            Name = model.Name,
            Level = model.Level,
            Building = model.Building,
            Rooms = model.Rooms,
        };
    }
}


public class RoomRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? RoomId { get; set; } 
 public virtual string? Name { get; set; } 
}

public class RoomResponse : RoomRequest {
    public static RoomResponse FromModel(Room model) {
        return new RoomResponse {
            Id = model.Id,
            RoomId = model.RoomId,
            Name = model.Name,
            Floor = model.Floor,
            Devices = model.Devices,
            Gateways = model.Gateways,
        };
    }
}


public class GatewayRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? GatewayId { get; set; } 
 public virtual string? SoftwareVersion { get; set; } 
 public virtual DeviceStatus? Status { get; set; } 
}

public class GatewayResponse : GatewayRequest {
    public static GatewayResponse FromModel(Gateway model) {
        return new GatewayResponse {
            Id = model.Id,
            GatewayId = model.GatewayId,
            SoftwareVersion = model.SoftwareVersion,
            Site = model.Site,
            Room = model.Room,
            Devices = model.Devices,
            EdgeApplications = model.EdgeApplications,
            Certificates = model.Certificates,
            DigitalTwin = model.DigitalTwin,
            NetworkProfiles = model.NetworkProfiles,
            Status = model.Status,
        };
    }
}


public class EdgeApplicationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? EdgeapplicationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Version { get; set; } 
 public virtual string? Image { get; set; } 
 public virtual DeploymentStatus? Status { get; set; } 
}

public class EdgeApplicationResponse : EdgeApplicationRequest {
    public static EdgeApplicationResponse FromModel(EdgeApplication model) {
        return new EdgeApplicationResponse {
            Id = model.Id,
            EdgeapplicationId = model.EdgeapplicationId,
            Name = model.Name,
            Version = model.Version,
            Image = model.Image,
            Gateway = model.Gateway,
            Status = model.Status,
        };
    }
}


public class NetworkProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? NetworkprofileId { get; set; } 
 public virtual string? ProfileName { get; set; } 
 public virtual string? Ssid { get; set; } 
 public virtual string? Apn { get; set; } 
 public virtual ConnectivityType? ConnectivityType { get; set; } 
}

public class NetworkProfileResponse : NetworkProfileRequest {
    public static NetworkProfileResponse FromModel(NetworkProfile model) {
        return new NetworkProfileResponse {
            Id = model.Id,
            NetworkprofileId = model.NetworkprofileId,
            ProfileName = model.ProfileName,
            Ssid = model.Ssid,
            Apn = model.Apn,
            Device = model.Device,
            Gateway = model.Gateway,
            SimCard = model.SimCard,
            ConnectivityType = model.ConnectivityType,
        };
    }
}


public class SimCardRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? SimcardId { get; set; } 
 public virtual string? Iccid { get; set; } 
 public virtual string? Imsi { get; set; } 
 public virtual string? Carrier { get; set; } 
 public virtual SimStatus? Status { get; set; } 
}

public class SimCardResponse : SimCardRequest {
    public static SimCardResponse FromModel(SimCard model) {
        return new SimCardResponse {
            Id = model.Id,
            SimcardId = model.SimcardId,
            Iccid = model.Iccid,
            Imsi = model.Imsi,
            Carrier = model.Carrier,
            NetworkProfiles = model.NetworkProfiles,
            Tenant = model.Tenant,
            ConnectivityPlan = model.ConnectivityPlan,
            Status = model.Status,
        };
    }
}


public class ConnectivityPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? ConnectivityplanId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? DataCapMB { get; set; } 
 public virtual int? BillingCycleDays { get; set; } 
}

public class ConnectivityPlanResponse : ConnectivityPlanRequest {
    public static ConnectivityPlanResponse FromModel(ConnectivityPlan model) {
        return new ConnectivityPlanResponse {
            Id = model.Id,
            ConnectivityplanId = model.ConnectivityplanId,
            Name = model.Name,
            DataCapMB = model.DataCapMB,
            BillingCycleDays = model.BillingCycleDays,
            SimCards = model.SimCards,
            Tenant = model.Tenant,
        };
    }
}


public class MessagingEndpointRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? MessagingendpointId { get; set; } 
 public virtual string? Host { get; set; } 
 public virtual int? Port { get; set; } 
 public virtual bool? Secure { get; set; } 
 public virtual MessagingProtocol? Protocol { get; set; } 
}

public class MessagingEndpointResponse : MessagingEndpointRequest {
    public static MessagingEndpointResponse FromModel(MessagingEndpoint model) {
        return new MessagingEndpointResponse {
            Id = model.Id,
            MessagingendpointId = model.MessagingendpointId,
            Host = model.Host,
            Port = model.Port,
            Secure = model.Secure,
            Tenant = model.Tenant,
            Streams = model.Streams,
            Protocol = model.Protocol,
        };
    }
}


public class AccessPolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? AccesspolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Scope { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
}

public class AccessPolicyResponse : AccessPolicyRequest {
    public static AccessPolicyResponse FromModel(AccessPolicy model) {
        return new AccessPolicyResponse {
            Id = model.Id,
            AccesspolicyId = model.AccesspolicyId,
            Name = model.Name,
            Scope = model.Scope,
            ExpiresAt = model.ExpiresAt,
            Tenant = model.Tenant,
            ApiKeys = model.ApiKeys,
            Users = model.Users,
        };
    }
}


public class ApiKeyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? ApikeyId { get; set; } 
 public virtual string? KeyId { get; set; } 
 public virtual string? HashedSecret { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual DateTime? LastUsedAt { get; set; } 
}

public class ApiKeyResponse : ApiKeyRequest {
    public static ApiKeyResponse FromModel(ApiKey model) {
        return new ApiKeyResponse {
            Id = model.Id,
            ApikeyId = model.ApikeyId,
            KeyId = model.KeyId,
            HashedSecret = model.HashedSecret,
            CreatedAt = model.CreatedAt,
            LastUsedAt = model.LastUsedAt,
            AccessPolicy = model.AccessPolicy,
        };
    }
}


public class DeviceCertificateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? DevicecertificateId { get; set; } 
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime? NotBefore { get; set; } 
 public virtual DateTime? NotAfter { get; set; } 
 public virtual string? Fingerprint { get; set; } 
 public virtual CertificateType? CertificateType { get; set; } 
}

public class DeviceCertificateResponse : DeviceCertificateRequest {
    public static DeviceCertificateResponse FromModel(DeviceCertificate model) {
        return new DeviceCertificateResponse {
            Id = model.Id,
            DevicecertificateId = model.DevicecertificateId,
            SerialNumber = model.SerialNumber,
            NotBefore = model.NotBefore,
            NotAfter = model.NotAfter,
            Fingerprint = model.Fingerprint,
            Device = model.Device,
            Gateway = model.Gateway,
            CertificateType = model.CertificateType,
        };
    }
}


public class ProvisioningRecordRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? ProvisioningrecordId { get; set; } 
 public virtual DateTime? EnrolledAt { get; set; } 
 public virtual string? ProvisioningService { get; set; } 
 public virtual ProvisioningMethod? Method { get; set; } 
 public virtual ProvisioningStatus? Status { get; set; } 
}

public class ProvisioningRecordResponse : ProvisioningRecordRequest {
    public static ProvisioningRecordResponse FromModel(ProvisioningRecord model) {
        return new ProvisioningRecordResponse {
            Id = model.Id,
            ProvisioningrecordId = model.ProvisioningrecordId,
            EnrolledAt = model.EnrolledAt,
            ProvisioningService = model.ProvisioningService,
            Device = model.Device,
            Certificate = model.Certificate,
            Tenant = model.Tenant,
            Method = model.Method,
            Status = model.Status,
        };
    }
}


public class DigitalTwinRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? DigitaltwinId { get; set; } 
 public virtual string? TwinId { get; set; } 
 public virtual int? DesiredStateVersion { get; set; } 
 public virtual int? ReportedStateVersion { get; set; } 
 public virtual DateTime? LastSyncAt { get; set; } 
}

public class DigitalTwinResponse : DigitalTwinRequest {
    public static DigitalTwinResponse FromModel(DigitalTwin model) {
        return new DigitalTwinResponse {
            Id = model.Id,
            DigitaltwinId = model.DigitaltwinId,
            TwinId = model.TwinId,
            DesiredStateVersion = model.DesiredStateVersion,
            ReportedStateVersion = model.ReportedStateVersion,
            LastSyncAt = model.LastSyncAt,
            Device = model.Device,
            Gateway = model.Gateway,
            Template = model.Template,
            ChangeEvents = model.ChangeEvents,
        };
    }
}


public class TwinTemplateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TwintemplateId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
 public virtual string? Version { get; set; } 
}

public class TwinTemplateResponse : TwinTemplateRequest {
    public static TwinTemplateResponse FromModel(TwinTemplate model) {
        return new TwinTemplateResponse {
            Id = model.Id,
            TwintemplateId = model.TwintemplateId,
            Name = model.Name,
            SchemaUri = model.SchemaUri,
            Version = model.Version,
            DeviceModels = model.DeviceModels,
        };
    }
}


public class TwinChangeEventRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? TwinchangeeventId { get; set; } 
 public virtual string? EventId { get; set; } 
 public virtual DateTime? OccurredAt { get; set; } 
 public virtual TwinChangeType? ChangeType { get; set; } 
}

public class TwinChangeEventResponse : TwinChangeEventRequest {
    public static TwinChangeEventResponse FromModel(TwinChangeEvent model) {
        return new TwinChangeEventResponse {
            Id = model.Id,
            TwinchangeeventId = model.TwinchangeeventId,
            EventId = model.EventId,
            OccurredAt = model.OccurredAt,
            Twin = model.Twin,
            ChangeType = model.ChangeType,
        };
    }
}


public class MaintenanceTicketRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? MaintenanceticketId { get; set; } 
 public virtual string? TicketNumber { get; set; } 
 public virtual DateTime? OpenedAt { get; set; } 
 public virtual DateTime? ClosedAt { get; set; } 
 public virtual MaintenancePriority? Priority { get; set; } 
 public virtual MaintenanceStatus? Status { get; set; } 
}

public class MaintenanceTicketResponse : MaintenanceTicketRequest {
    public static MaintenanceTicketResponse FromModel(MaintenanceTicket model) {
        return new MaintenanceTicketResponse {
            Id = model.Id,
            MaintenanceticketId = model.MaintenanceticketId,
            TicketNumber = model.TicketNumber,
            OpenedAt = model.OpenedAt,
            ClosedAt = model.ClosedAt,
            Device = model.Device,
            Tenant = model.Tenant,
            Priority = model.Priority,
            Status = model.Status,
        };
    }
}


public class DataRetentionPolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? DataretentionpolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? RetentionDays { get; set; } 
}

public class DataRetentionPolicyResponse : DataRetentionPolicyRequest {
    public static DataRetentionPolicyResponse FromModel(DataRetentionPolicy model) {
        return new DataRetentionPolicyResponse {
            Id = model.Id,
            DataretentionpolicyId = model.DataretentionpolicyId,
            Name = model.Name,
            RetentionDays = model.RetentionDays,
            Tenant = model.Tenant,
            Streams = model.Streams,
        };
    }
}


public class SoftwareUpdateCampaignRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? SoftwareupdatecampaignId { get; set; } 
 public virtual string? CampaignCode { get; set; } 
 public virtual DateTime? ScheduledStart { get; set; } 
 public virtual DateTime? ScheduledEnd { get; set; } 
 public virtual UpdateCampaignStatus? Status { get; set; } 
}

public class SoftwareUpdateCampaignResponse : SoftwareUpdateCampaignRequest {
    public static SoftwareUpdateCampaignResponse FromModel(SoftwareUpdateCampaign model) {
        return new SoftwareUpdateCampaignResponse {
            Id = model.Id,
            SoftwareupdatecampaignId = model.SoftwareupdatecampaignId,
            CampaignCode = model.CampaignCode,
            ScheduledStart = model.ScheduledStart,
            ScheduledEnd = model.ScheduledEnd,
            FirmwareRelease = model.FirmwareRelease,
            DeviceGroup = model.DeviceGroup,
            Executions = model.Executions,
            Status = model.Status,
        };
    }
}


public class SoftwareUpdateExecutionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? SoftwareupdateexecutionId { get; set; } 
 public virtual DateTime? StartedAt { get; set; } 
 public virtual DateTime? CompletedAt { get; set; } 
 public virtual UpdateStatus? Status { get; set; } 
}

public class SoftwareUpdateExecutionResponse : SoftwareUpdateExecutionRequest {
    public static SoftwareUpdateExecutionResponse FromModel(SoftwareUpdateExecution model) {
        return new SoftwareUpdateExecutionResponse {
            Id = model.Id,
            SoftwareupdateexecutionId = model.SoftwareupdateexecutionId,
            StartedAt = model.StartedAt,
            CompletedAt = model.CompletedAt,
            Campaign = model.Campaign,
            Device = model.Device,
            Status = model.Status,
        };
    }
}


public class DeviceGroupRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? DevicegroupId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Criteria { get; set; } 
}

public class DeviceGroupResponse : DeviceGroupRequest {
    public static DeviceGroupResponse FromModel(DeviceGroup model) {
        return new DeviceGroupResponse {
            Id = model.Id,
            DevicegroupId = model.DevicegroupId,
            Name = model.Name,
            Criteria = model.Criteria,
            Tenant = model.Tenant,
            Devices = model.Devices,
        };
    }
}


public class UsageRecordRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual long? UsagerecordId { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual int? MessagesSent { get; set; } 
 public virtual int? DataVolumeMB { get; set; } 
}

public class UsageRecordResponse : UsageRecordRequest {
    public static UsageRecordResponse FromModel(UsageRecord model) {
        return new UsageRecordResponse {
            Id = model.Id,
            UsagerecordId = model.UsagerecordId,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            MessagesSent = model.MessagesSent,
            DataVolumeMB = model.DataVolumeMB,
            Tenant = model.Tenant,
            Device = model.Device,
            ConnectivityPlan = model.ConnectivityPlan,
        };
    }
}

