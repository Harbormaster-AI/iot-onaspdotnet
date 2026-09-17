using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AlertRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

										