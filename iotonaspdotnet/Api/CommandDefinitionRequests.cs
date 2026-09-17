namespace iotonaspdotnet.Api;

, String, Uri_, Uri_, Integer
, Guid DeviceModelId
public record CreateCommandDefinitionRequest(string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
public record UpdateCommandDefinitionRequest(string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
public record CommandDefinitionResponse(Guid Id, string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
