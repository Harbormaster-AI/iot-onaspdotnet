namespace iotonaspdotnet.Api;


public record CreateSoftwareUpdateExecutionRequest( , DateTime, DateTime, UpdateStatus , Guid SoftwareUpdateCampaignId, Guid IoTDeviceId );
public record UpdateSoftwareUpdateExecutionRequest( , DateTime, DateTime, UpdateStatus , Guid SoftwareUpdateCampaignId, Guid IoTDeviceId );
public record SoftwareUpdateExecutionResponse( Guid Id, , DateTime, DateTime, UpdateStatus , Guid SoftwareUpdateCampaignId, Guid IoTDeviceId );
