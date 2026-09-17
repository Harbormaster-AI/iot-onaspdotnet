namespace iotonaspdotnet.Api.CommandDefinition;

public record CreateCommandDefinitionRequest(string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
public record UpdateCommandDefinitionRequest(string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
public record CommandDefinitionResponse(Guid Id, string CommandDefinitionNumber, decimal Balance, Guid DeviceModelId);
