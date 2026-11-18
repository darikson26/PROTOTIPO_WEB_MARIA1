namespace PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;

public sealed class PartyId
{
    private PartyId()
    {
        Value = Guid.Empty;
    }

    public PartyId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Patient identifier cannot be empty.", nameof(value));
        }

        Value = value;
    }

    public Guid Value { get; private set; }

    public override string ToString() => Value.ToString();
}
