namespace iotonaspdotnet.Domain;

public class DeviceCertificate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long DevicecertificateId { get; set; }
 public virtual string SerialNumber { get; set; }
 public virtual DateTime NotBefore { get; set; }
 public virtual DateTime NotAfter { get; set; }
 public virtual string Fingerprint { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
 public virtual CertificateType CertificateType { get; set; }
}
