using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class IoTDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

										