namespace iotonaspdotnet.Api.SoftwareUpdateCampaign;

public record CreateSoftwareUpdateCampaignRequest(string SoftwareUpdateCampaignNumber, decimal Balance , , Guid FirmwareReleaseIdGuid DeviceGroupId);
public record UpdateSoftwareUpdateCampaignRequest(string SoftwareUpdateCampaignNumber, decimal Balance , , Guid FirmwareReleaseIdGuid DeviceGroupId);
public record SoftwareUpdateCampaignResponse(Guid Id, string SoftwareUpdateCampaignNumber, decimal Balance , , Guid FirmwareReleaseIdGuid DeviceGroupId);
