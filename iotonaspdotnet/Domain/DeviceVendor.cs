using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class DeviceVendor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DevicevendorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? HeadquartersCountry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<DeviceModel>? DeviceModels { get; set; } = new List<DeviceModel>()
public virtual ICollection<FirmwareRelease>? FirmwareReleases { get; set; } = new List<FirmwareRelease>()
public virtual ICollection<HardwareModule>? HardwareModules { get; set; } = new List<HardwareModule>()

    public static DeviceVendor FromRequest(DeviceVendorRequest request) {
        return new DeviceVendor {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            HeadquartersCountry = request.HeadquartersCountry,
            Website = request.Website,
        };
    }
}
