using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Resources;

public sealed class CreatePrescriptionResource
{
    [Required]
    public string PrescriptionNumber { get; init; } = string.Empty;

    [Required]
    public string PatientId { get; init; } = string.Empty;

    [Required]
    public DateOnly StartDate { get; init; }

    [Required]
    public DateOnly EndDate { get; init; }

    [Range(1, 4)]
    public int DailyQuantity { get; init; }

    [Required]
    public string ActiveIngredient { get; init; } = string.Empty;

    public string? Status { get; init; }

    public string? ClinicalNotes { get; init; }
}

public sealed record PrescriptionResource(
    int Id,
    string PrescriptionNumber,
    string PatientId,
    string StartDate,
    string EndDate,
    int DailyQuantity,
    string ActiveIngredient,
    string Status,
    string? ClinicalNotes);
