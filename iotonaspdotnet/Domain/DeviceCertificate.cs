using iotonaspdotnet.Domain.IoTDevices;
using iotonaspdotnet.Domain.Gateways;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class DeviceCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long devicecertificateId { get; set; }
 public virtual string serialNumber { get; set; }
 public virtual DateTime notBefore { get; set; }
 public virtual DateTime notAfter { get; set; }
 public virtual string fingerprint { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
 public virtual CertificateType CertificateType { get; set; }
}
