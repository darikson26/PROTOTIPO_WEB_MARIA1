using Microsoft.EntityFrameworkCore;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Repositories;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;

namespace PC29999.U202422620.Ro.Platform.Clinical.Infrastructure.Persistence;

public sealed class PrescriptionRepository(PrescriptionDbContext context) : IPrescriptionRepository
{
    public Task<bool> ExistsAsync(string prescriptionNumber, CancellationToken cancellationToken)
    {
        var identifier = new RoIdentifier(prescriptionNumber);
        return context.Prescriptions.AnyAsync(prescription => prescription.PrescriptionNumber == identifier, cancellationToken);
    }

    public async Task AddAsync(Prescription prescription, CancellationToken cancellationToken)
    {
        context.Prescriptions.Add(prescription);
        await context.SaveChangesAsync(cancellationToken);
    }
}
