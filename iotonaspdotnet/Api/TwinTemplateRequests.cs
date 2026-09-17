namespace iotonaspdotnet.Api;

, String, Uri_, String

public record CreateTwinTemplateRequest(string TwinTemplateNumber, decimal Balance);
public record UpdateTwinTemplateRequest(string TwinTemplateNumber, decimal Balance);
public record TwinTemplateResponse(Guid Id, string TwinTemplateNumber, decimal Balance);
