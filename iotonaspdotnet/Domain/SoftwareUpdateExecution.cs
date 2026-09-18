using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long SoftwareupdateexecutionId { get; set; }
 public virtual DateTime StartedAt { get; set; }
 public virtual DateTime CompletedAt { get; set; }
public virtual SoftwareUpdateCampaign Campaign { get; set; }
public virtual IoTDevice Device { get; set; }
 public virtual UpdateStatus Status { get; set; }

    public static SoftwareUpdateExecution FromRequest(SoftwareUpdateExecutionRequest request) {
        return new SoftwareUpdateExecution {
            Id = request.Id,
            StartedAt = request.StartedAt,
            CompletedAt = request.CompletedAt,
            Status = request.Status,
        };
    }
}
