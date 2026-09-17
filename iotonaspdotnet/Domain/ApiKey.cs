using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ApiKey
{
    public Guid Id { get; set; } = Guid.NewGuid();

										