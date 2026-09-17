using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class FirmwareRelease
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual CommandDefinition firmwarereleaseId { get; set; }
								 public virtual CommandDefinition version { get; set; }
								 public virtual CommandDefinition releaseDate { get; set; }
								 public virtual CommandDefinition releaseNotes { get; set; }
								 public virtual CommandDefinition checksum { get; set; }
								public virtual DeviceModel DeviceModel { get; set; }
			}
