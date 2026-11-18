using System.Globalization;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Aggregates;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;
using PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Resources;

namespace PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Transform;

public static class PrescriptionAssembler
{
    public static PrescriptionResource ToResource(Prescription prescription) => new(
        prescription.Id,
        prescription.PrescriptionNumber.Value,
        prescription.PatientId.Value.ToString(),
        prescription.TreatmentPeriod.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        prescription.TreatmentPeriod.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        prescription.DosageLimit.DailyQuantity,
        prescription.DosageLimit.ActiveIngredient,
        prescription.Status.ToString(),
        prescription.ClinicalNotes);

    public static bool TryReadStatus(string? value, out PrescriptionStatus status)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            status = PrescriptionStatus.Draft;
            return true;
        }

        return Enum.TryParse(value, true, out status) && Enum.IsDefined(status);
    }
}
