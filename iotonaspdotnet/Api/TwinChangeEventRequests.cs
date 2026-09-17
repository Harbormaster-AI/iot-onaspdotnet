namespace iotonaspdotnet.Api;

, String, DateTime, TwinChangeType
, Guid DigitalTwinId
public record CreateTwinChangeEventRequest(string TwinChangeEventNumber, decimal Balance, Guid DigitalTwinId);
public record UpdateTwinChangeEventRequest(string TwinChangeEventNumber, decimal Balance, Guid DigitalTwinId);
public record TwinChangeEventResponse(Guid Id, string TwinChangeEventNumber, decimal Balance, Guid DigitalTwinId);
