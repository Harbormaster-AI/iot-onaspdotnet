namespace iotonaspdotnet.Api;

public record CreateTelemetrySchemaRequest(string TelemetrySchemaNumber, decimal Balance);
public record UpdateTelemetrySchemaRequest(string TelemetrySchemaNumber, decimal Balance);
public record TelemetrySchemaResponse(Guid Id, string TelemetrySchemaNumber, decimal Balance);
