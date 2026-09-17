namespace iotonaspdotnet.Api;

, String, Uri_, TelemetryEncoding

public record CreateTelemetrySchemaRequest(string TelemetrySchemaNumber, decimal Balance);
public record UpdateTelemetrySchemaRequest(string TelemetrySchemaNumber, decimal Balance);
public record TelemetrySchemaResponse(Guid Id, string TelemetrySchemaNumber, decimal Balance);
