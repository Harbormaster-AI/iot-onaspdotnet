using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IHardwareModuleRepository
{
    Task<HardwareModule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<HardwareModule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(HardwareModule hardwareModule, CancellationToken cancellationToken);
    Task UpdateAsync(HardwareModule hardwareModule, CancellationToken cancellationToken);
    Task DeleteAsync(HardwareModule hardwareModule, CancellationToken cancellationToken);


}
