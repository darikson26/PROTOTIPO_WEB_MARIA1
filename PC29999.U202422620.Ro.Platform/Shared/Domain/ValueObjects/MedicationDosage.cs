namespace PC29999.U202422620.Ro.Platform.Shared.Domain.ValueObjects;

public sealed class MedicationDosage
{
    private MedicationDosage()
    {
        ActiveIngredient = string.Empty;
    }

    public MedicationDosage(int dailyQuantity, string activeIngredient)
    {
        if (dailyQuantity <= 0 || dailyQuantity > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(dailyQuantity), "Daily quantity must be between 1 and 4.");
        }

        if (string.IsNullOrWhiteSpace(activeIngredient) || activeIngredient.Length < 3 || activeIngredient.Length > 15 || activeIngredient.Any(character => !char.IsAsciiLetterUpper(character)))
        {
            throw new ArgumentException("Active ingredient must contain 3 to 15 uppercase letters.", nameof(activeIngredient));
        }

        DailyQuantity = dailyQuantity;
        ActiveIngredient = activeIngredient;
    }

    public int DailyQuantity { get; private set; }

    public string ActiveIngredient { get; private set; }

    public int CalculateTotalDoses(int totalDays)
    {
        if (totalDays < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalDays));
        }

        return DailyQuantity * totalDays;
    }
}
