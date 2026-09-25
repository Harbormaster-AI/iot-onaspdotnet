using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IIoTDeviceRepository
{
    Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task DeleteAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);

    Task AddToSensorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSensorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToActuatorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromActuatorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTelemetryStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTelemetryStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCommandInvocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCommandInvocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDeviceGroupsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDeviceGroupsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
