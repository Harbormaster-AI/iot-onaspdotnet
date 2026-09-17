namespace iotonaspdotnet.Api;


public record CreateTwinChangeEventRequest( , String, DateTime, TwinChangeType , Guid DigitalTwinId );
public record UpdateTwinChangeEventRequest( , String, DateTime, TwinChangeType , Guid DigitalTwinId );
public record TwinChangeEventResponse( Guid Id, , String, DateTime, TwinChangeType , Guid DigitalTwinId );
