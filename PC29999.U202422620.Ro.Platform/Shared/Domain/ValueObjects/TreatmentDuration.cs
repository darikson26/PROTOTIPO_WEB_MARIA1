namespace PC29999.U202422620.Ro.Platform.Shared.Domain.ValueObjects;

public sealed class TreatmentDuration
{
    private TreatmentDuration()
    {
    }

    public TreatmentDuration(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
        {
            throw new ArgumentException("End date must be after start date.");
        }

        if (endDate.DayNumber - startDate.DayNumber + 1 > 90)
        {
            throw new ArgumentException("Treatment duration cannot exceed 90 calendar days.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public bool IsActiveOn(DateOnly checkDate) => checkDate >= StartDate && checkDate <= EndDate;
}
