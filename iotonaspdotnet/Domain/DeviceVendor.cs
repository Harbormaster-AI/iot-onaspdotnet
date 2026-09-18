using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class DeviceVendor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long DevicevendorId { get; set; }
 public virtual string Name { get; set; }
 public virtual string LegalName { get; set; }
 public virtual string HeadquartersCountry { get; set; }
 public virtual string Website { get; set; }
public virtual DeviceModel DeviceModels { get; set; }
public virtual FirmwareRelease FirmwareReleases { get; set; }
public virtual HardwareModule HardwareModules { get; set; }

    public static DeviceVendor FromRequest(DeviceVendorRequest request) {
        return new DeviceVendor {
            Id = model.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
}
