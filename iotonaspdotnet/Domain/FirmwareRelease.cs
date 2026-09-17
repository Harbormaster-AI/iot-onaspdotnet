namespace iotonaspdotnet.Domain;

public class FirmwareRelease
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long FirmwarereleaseId { get; set; }
 public virtual FirmwareVersion Version { get; set; }
 public virtual DateOnly ReleaseDate { get; set; }
 public virtual string ReleaseNotes { get; set; }
 public virtual Checksum Checksum { get; set; }
public virtual DeviceModel DeviceModel { get; set; }
}
