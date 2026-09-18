using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class HardwareModule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? HardwaremoduleId { get; set; } 
 public virtual string? ModuleCode { get; set; } 
 public virtual Uri_? DatasheetUri { get; set; } 
public virtual DeviceVendor Vendor { get; set; } 
 public virtual ModuleType? ModuleType { get; set; } 

    public static HardwareModule FromRequest(HardwareModuleRequest request) {
        return new HardwareModule {
            Id = request.Id,
            ModuleCode = request.ModuleCode,
            DatasheetUri = request.DatasheetUri,
            ModuleType = request.ModuleType,
        };
    }
}
