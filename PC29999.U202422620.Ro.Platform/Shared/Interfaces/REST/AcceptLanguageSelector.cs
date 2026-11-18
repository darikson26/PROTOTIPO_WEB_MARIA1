namespace PC29999.U202422620.Ro.Platform.Shared.Interfaces.REST;

public static class AcceptLanguageSelector
{
    public static bool IsSpanish(string? headerValue)
    {
        var languages = (headerValue ?? string.Empty).Split(',')
            .Select(item => item.Split(';'))
            .Select(parts => new
            {
                Name = parts[0].Trim().ToLowerInvariant(),
                Quality = parts.Length > 1 && parts[1].Trim().StartsWith("q=", StringComparison.OrdinalIgnoreCase)
                    && double.TryParse(parts[1].Trim()[2..], System.Globalization.CultureInfo.InvariantCulture, out var quality)
                    ? quality
                    : 1.0
            })
            .OrderByDescending(item => item.Quality);

        foreach (var language in languages)
        {
            if (language.Name is "es" or "es-pe")
            {
                return true;
            }

            if (language.Name is "en" or "en-us")
            {
                return false;
            }
        }

        return false;
    }
}
