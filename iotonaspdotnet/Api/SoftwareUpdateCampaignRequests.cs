namespace iotonaspdotnet.Api;


public record CreateSoftwareUpdateCampaignRequest( , String, DateTime, DateTime, UpdateCampaignStatus , Guid FirmwareReleaseId, Guid DeviceGroupId );
public record UpdateSoftwareUpdateCampaignRequest( , String, DateTime, DateTime, UpdateCampaignStatus , Guid FirmwareReleaseId, Guid DeviceGroupId );
public record SoftwareUpdateCampaignResponse( Guid Id, , String, DateTime, DateTime, UpdateCampaignStatus , Guid FirmwareReleaseId, Guid DeviceGroupId );
