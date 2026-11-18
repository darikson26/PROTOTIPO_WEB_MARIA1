using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Exceptions;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Repositories;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;
using PC29999.U202422620.Ro.Platform.Shared.Domain.ValueObjects;

namespace PC29999.U202422620.Ro.Platform.Clinical.Application;

public sealed class CreatePrescriptionService(IPrescriptionRepository repository)
{
    public async Task<Prescription> ExecuteAsync(Guid patientIdFromRoute, CreatePrescriptionCommand command, CancellationToken cancellationToken)
    {
        if (patientIdFromRoute != command.PatientId)
        {
            throw new PrescriptionConflictException("The patient identifier in the URL must match the patient identifier in the request body.");
        }

        var prescriptionNumber = new RoIdentifier(command.PrescriptionNumber);
        if (await repository.ExistsAsync(prescriptionNumber.Value, cancellationToken))
        {
            throw new PrescriptionConflictException("A prescription with this prescription number already exists.");
        }

        var prescription = new Prescription(
            prescriptionNumber,
            new PartyId(command.PatientId),
            new TreatmentDuration(command.StartDate, command.EndDate),
            new MedicationDosage(command.DailyQuantity, command.ActiveIngredient),
            command.Status,
            command.ClinicalNotes);

        await repository.AddAsync(prescription, cancellationToken);
        return prescription;
    }
}

public sealed record CreatePrescriptionCommand(
    string PrescriptionNumber,
    Guid PatientId,
    DateOnly StartDate,
    DateOnly EndDate,
    int DailyQuantity,
    string ActiveIngredient,
    PrescriptionStatus Status,
    string? ClinicalNotes);
