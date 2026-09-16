namespace iotonaspdotnet.Api.DeviceVendor;

public record CreateDeviceVendorRequest(string DeviceVendorNumber, decimal Balance );
public record UpdateDeviceVendorRequest(string DeviceVendorNumber, decimal Balance );
public record DeviceVendorResponse(Guid Id, string DeviceVendorNumber, decimal Balance );
