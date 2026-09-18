namespace iotonaspdotnet.Domain;
    [ComplexType]
    public record DeviceId(
    string Value
    );
    [ComplexType]
    public record FirmwareVersion(
    string Value
    );
    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );
    [ComplexType]
    public record Uri_(
    string Value
    );
    [ComplexType]
    public record TopicName(
    string Value
    );
    [ComplexType]
    public record Checksum(
    string Algorithm,
    string Value
    );
