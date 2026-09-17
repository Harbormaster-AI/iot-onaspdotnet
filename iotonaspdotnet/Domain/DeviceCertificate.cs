using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DeviceCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual AccessPolicy devicecertificateId { get; set; }
								 public virtual AccessPolicy serialNumber { get; set; }
								 public virtual AccessPolicy notBefore { get; set; }
								 public virtual AccessPolicy notAfter { get; set; }
								 public virtual AccessPolicy fingerprint { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual Gateway Gateway { get; set; }
								 public virtual Gateway CertificateType { get; set; }
			}
