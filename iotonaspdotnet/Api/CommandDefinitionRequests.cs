namespace iotonaspdotnet.Api;


public record CreateCommandDefinitionRequest( , String, Uri_, Uri_, Integer , Guid DeviceModelId );
public record UpdateCommandDefinitionRequest( , String, Uri_, Uri_, Integer , Guid DeviceModelId );
public record CommandDefinitionResponse( Guid Id, , String, Uri_, Uri_, Integer , Guid DeviceModelId );
