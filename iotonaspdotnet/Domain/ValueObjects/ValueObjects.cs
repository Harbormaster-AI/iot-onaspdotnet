namespace iotonaspdotnet.Domain.ValueObjects;
    public record DeviceId(
    string Value
    );
    public record FirmwareVersion(
    string Value
    );
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );
    public record Uri(
    string Value
    );
    public record TopicName(
    string Value
    );
    public record Checksum(
    string Algorithm,
    string Value
    );
