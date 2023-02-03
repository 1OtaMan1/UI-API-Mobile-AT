using System.Text;
using Core.Extensions;

namespace Core.Utils;

public static class StringUtils
{
    public const string EmailDomain = "mailforspam.com";

#pragma warning disable S1075 // URIs should not be hardcoded
    public const string UrlTemplate = "http://{0}.com";
#pragma warning restore S1075 // URIs should not be hardcoded

    public static string GetRandomEmail() => $"{"email".AddRandomAlphabetical()}@{EmailDomain}";

    public static string GetRandomUrl() => string.Format(UrlTemplate, GetRandomAlphabetical(10, randomCase: false));

    public static string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (var i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }

    public static string GetRandomAlphabetical(int length, bool randomCase = true)
    {
        var randomAlphabetical = new StringBuilder();
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var i = 0; i < length; i++)
        {
            var character = random.Next(97, 123);

            if (randomCase && random.Next() % 2 == 0)
            {
                character -= 32;
            }

            randomAlphabetical.Append(Convert.ToChar(character));
        }

        return randomAlphabetical.ToString();
    }

    public static string GetRandomSpecial(int length)
    {
        var randomSpecial = new StringBuilder();
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var i = 0; i < length; i++)
        {
            var character = random.Next(33, 44);

            randomSpecial.Append(Convert.ToChar(character));
        }

        return randomSpecial.ToString();
    }

    public static string Prepare(string name)
    {
        const int maxNameLength = 58;
        const int lastDisplayedCharacterPosition = 49;

        return name.Length <= maxNameLength ? name : name[..lastDisplayedCharacterPosition] + "...";
    }

    public static string SubstringFileExtension(string name)
    {
        if (name.Contains('.'))
        {
            name = name[..name.LastIndexOf('.')].Trim();
        }

        return name;
    }

    public static List<int> GetUiElementCoordinates(string elementStyleAttribute)
    {
        var elementIndex = elementStyleAttribute.IndexOf(")", StringComparison.Ordinal);
        var coordinates = elementStyleAttribute[..elementIndex]
            .Replace("transform: translate3d(", "")
            .Replace("px, 0px", "")
            .Replace("px", "")
            .Replace(" ", "")
            .Replace("z-index:12;", "")
            .Replace("z-index:15;", ""); // ToDo - replace last 2 lines with Regex that will delete "z-index:value;"
        return coordinates.Split(',').Select(int.Parse).ToList();
    }
}