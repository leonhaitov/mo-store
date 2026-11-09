using System.Text.RegularExpressions;

namespace MO.Store.Domain.Common;

public static partial class Slug
{
    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]
    private static partial Regex Pattern();

    public static string Validate(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (!Pattern().IsMatch(value))
            throw new ArgumentException(
                $"Slug '{value}' must contain only lowercase letters, digits and single hyphens.",
                nameof(value));

        return value;
    }
}
