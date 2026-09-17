using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class HardwareModule
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual HardwareModule hardwaremoduleId { get; set; }
								 public virtual HardwareModule moduleCode { get; set; }
								 public virtual HardwareModule datasheetUri { get; set; }
								public virtual DeviceVendor Vendor { get; set; }
								 public virtual DeviceVendor ModuleType { get; set; }
			}
