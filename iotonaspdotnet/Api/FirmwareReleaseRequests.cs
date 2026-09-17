namespace iotonaspdotnet.Api;

public record CreateFirmwareReleaseRequest(string FirmwareReleaseNumber, decimal Balance, Guid DeviceModelId);
public record UpdateFirmwareReleaseRequest(string FirmwareReleaseNumber, decimal Balance, Guid DeviceModelId);
public record FirmwareReleaseResponse(Guid Id, string FirmwareReleaseNumber, decimal Balance, Guid DeviceModelId);
