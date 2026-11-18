using PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;
using PC29999.U202422620.Ro.Platform.Shared.Domain.ValueObjects;

namespace PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;

public sealed class Prescription
{
    private Prescription()
    {
        PrescriptionNumber = null!;
        PatientId = null!;
        TreatmentPeriod = null!;
        DosageLimit = null!;
    }

    public Prescription(RoIdentifier prescriptionNumber, PartyId patientId, TreatmentDuration treatmentPeriod, MedicationDosage dosageLimit, PrescriptionStatus status, string? clinicalNotes)
    {
        PrescriptionNumber = prescriptionNumber;
        PatientId = patientId;
        TreatmentPeriod = treatmentPeriod;
        DosageLimit = dosageLimit;
        Status = status;
        ClinicalNotes = clinicalNotes;
        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public int Id { get; private set; }

    public RoIdentifier PrescriptionNumber { get; private set; }

    public PartyId PatientId { get; private set; }

    public TreatmentDuration TreatmentPeriod { get; private set; }

    public MedicationDosage DosageLimit { get; private set; }

    public PrescriptionStatus Status { get; private set; }

    public string? ClinicalNotes { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}
