namespace iotonaspdotnet.Api;


public record CreateDeviceVendorRequest( , String, String, String, String  );
public record UpdateDeviceVendorRequest( , String, String, String, String  );
public record DeviceVendorResponse( Guid Id, , String, String, String, String  );
