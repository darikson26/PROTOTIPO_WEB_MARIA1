using System.Text.RegularExpressions;

namespace PC29999.U202422620.Ro.Platform.Clinical.Domain.ValueObjects;

public sealed class RoIdentifier
{
    private static readonly Regex Format = new("^RX-[0-9]{6}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private RoIdentifier()
    {
        Value = string.Empty;
    }

    public RoIdentifier(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Format.IsMatch(value))
        {
            throw new ArgumentException("Prescription number must have the format RX- followed by six digits.", nameof(value));
        }

        Value = value;
    }

    public string Value { get; private set; }

    public override string ToString() => Value;
}
