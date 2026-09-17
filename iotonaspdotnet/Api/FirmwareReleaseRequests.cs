namespace iotonaspdotnet.Api;


public record CreateFirmwareReleaseRequest( , FirmwareVersion, Date, String, Checksum , Guid DeviceModelId );
public record UpdateFirmwareReleaseRequest( , FirmwareVersion, Date, String, Checksum , Guid DeviceModelId );
public record FirmwareReleaseResponse( Guid Id, , FirmwareVersion, Date, String, Checksum , Guid DeviceModelId );
