using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class FirmwareRelease
{
    public Guid Id { get; set; } = Guid.NewGuid();

													public virtual long firmwarereleaseId { get; set; }
											 public virtual FirmwareVersion version { get; set; }
											 public virtual DateOnly releaseDate { get; set; }
											 public virtual string releaseNotes { get; set; }
											 public virtual Checksum checksum { get; set; }
											public virtual DeviceModel DeviceModel { get; set; }
			}
