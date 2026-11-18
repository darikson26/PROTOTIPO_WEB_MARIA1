using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;

namespace PC29999.U202422620.Ro.Platform.Clinical.Domain.Repositories;

public interface IPrescriptionRepository
{
    Task<bool> ExistsAsync(string prescriptionNumber, CancellationToken cancellationToken);

    Task AddAsync(Prescription prescription, CancellationToken cancellationToken);
}
