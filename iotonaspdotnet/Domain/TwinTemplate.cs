using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TwinTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TwinChangeEvent twintemplateId { get; set; }
								 public virtual TwinChangeEvent name { get; set; }
								 public virtual TwinChangeEvent schemaUri { get; set; }
								 public virtual TwinChangeEvent version { get; set; }
								public virtual DeviceModel DeviceModels { get; set; }
			}
