using System.Text;
using System.Text.RegularExpressions;

namespace Core.Extensions;

public static class StringExtensions
{
    public static string AddRandomAlphabetical(this string currentString, int length = 10, bool randomCase = true)
    {
        var randomAlphabetical = new StringBuilder();
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var i = 0; i < length; i++)
        {
            var character = random.Next(97, 123);

            if (randomCase && random.Next() % 2 == 0) // divide by two - 50% percent possibility that we get uppercase
            {
                character -= 32;
            }

            randomAlphabetical.Append(Convert.ToChar(character));
        }

        return $"{currentString}{randomAlphabetical}";
    }

    public static string RemoveHtmlTags(this string currentString)
    {
        return string.IsNullOrEmpty(currentString)
            ? currentString
            : Regex.Replace(currentString, "<.*?>", string.Empty).Trim();
    }

    public static string ReplaceMultiSpaces(this string currentString)
    {
        return Regex.Replace(currentString, "[ ]{2,}", " ");
    }
}