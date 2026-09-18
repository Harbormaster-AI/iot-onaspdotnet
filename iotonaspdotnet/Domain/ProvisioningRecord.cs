using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class ProvisioningRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProvisioningrecordId { get; set; } 
 public virtual DateTime? EnrolledAt { get; set; } 
 public virtual string? ProvisioningService { get; set; } 
public virtual IoTDevice? Device { get; set; } 
public virtual DeviceCertificate? Certificate { get; set; } 
public virtual Tenant? Tenant { get; set; } 
 public virtual ProvisioningMethod? Method { get; set; } 
 public virtual ProvisioningStatus? Status { get; set; } 

    public static ProvisioningRecord FromRequest(ProvisioningRecordRequest request) {
        return new ProvisioningRecord {
            Id = request.Id,
            EnrolledAt = request.EnrolledAt,
            ProvisioningService = request.ProvisioningService,
            Method = request.Method,
            Status = request.Status,
        };
    }
}
