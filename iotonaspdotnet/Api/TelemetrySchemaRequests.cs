namespace iotonaspdotnet.Api;


public record CreateTelemetrySchemaRequest( , String, Uri_, TelemetryEncoding  );
public record UpdateTelemetrySchemaRequest( , String, Uri_, TelemetryEncoding  );
public record TelemetrySchemaResponse( Guid Id, , String, Uri_, TelemetryEncoding  );
