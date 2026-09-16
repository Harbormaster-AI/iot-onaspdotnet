namespace iotonaspdotnet.Api.SoftwareUpdateCampaign;

public record CreateSoftwareUpdateCampaignRequest(string SoftwareUpdateCampaignNumber, decimal Balance, Guid FirmwareReleaseId, Guid DeviceGroupId);
public record UpdateSoftwareUpdateCampaignRequest(string SoftwareUpdateCampaignNumber, decimal Balance, Guid FirmwareReleaseId, Guid DeviceGroupId);
public record SoftwareUpdateCampaignResponse(Guid Id, string SoftwareUpdateCampaignNumber, decimal Balance, Guid FirmwareReleaseId, Guid DeviceGroupId);
