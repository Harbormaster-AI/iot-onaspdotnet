namespace iotonaspdotnet.Api.SoftwareUpdateExecution;

public record CreateSoftwareUpdateExecutionRequest(string SoftwareUpdateExecutionNumber, decimal Balance, Guid SoftwareUpdateCampaignId, Guid IoTDeviceId);
public record UpdateSoftwareUpdateExecutionRequest(string SoftwareUpdateExecutionNumber, decimal Balance, Guid SoftwareUpdateCampaignId, Guid IoTDeviceId);
public record SoftwareUpdateExecutionResponse(Guid Id, string SoftwareUpdateExecutionNumber, decimal Balance, Guid SoftwareUpdateCampaignId, Guid IoTDeviceId);
