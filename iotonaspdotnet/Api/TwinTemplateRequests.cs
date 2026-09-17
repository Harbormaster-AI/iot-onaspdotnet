namespace iotonaspdotnet.Api;

public record CreateTwinTemplateRequest(string TwinTemplateNumber, decimal Balance);
public record UpdateTwinTemplateRequest(string TwinTemplateNumber, decimal Balance);
public record TwinTemplateResponse(Guid Id, string TwinTemplateNumber, decimal Balance);
