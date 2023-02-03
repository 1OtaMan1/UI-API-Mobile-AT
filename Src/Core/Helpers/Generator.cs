using Bogus;
using WaffleGenerator;

namespace Core.Helpers;

public static class Generator
{
    public static Faker Faker => new();

    public static string Name()
    {
        var name = $"TAF {Fuzz()} {WaffleEngine.Title(Random())}";
        if (name.Length > 34)
        {
            name = name[..34].TrimEnd();
        }

        return name;
    }

    public static string Text(int length = 10) => Faker.Random.String2(length).Replace("'", "a");

    public static string TextWithSpaces(int length = 3000)
    {
        var paragraph = WaffleEngine.Title();
        var multiplier = (int) Math.Ceiling(Convert.ToDouble(length / paragraph.Length)) + 1;
        var text = string.Concat(Enumerable.Repeat(paragraph, multiplier))[..length].Trim();
        if (text.Length < length)
        {
            text += Text(length - text.Length);
        }

        return text.Replace("'", "a");
    }

    public static string Alphanumeric(int length = 10) => Faker.Random.AlphaNumeric(length);

    public static string SpecialCharacters(int length = 10) => Faker.Random.String(length, '\u2190', '\u21FF');

    public static int RandomNumber(int startValue = 0, int endValue = 9) => Faker.Random.Int(startValue, endValue);

    public static int TwoDigitNumber() => Faker.Random.Int(10, 99);

    public static string RandomLongNumberAsString(int length = 10)
    {
        var number = "";
        for (var i = 0; i < length; i++)
        {
            number += RandomNumber();
        }

        return number;
    }

    public static Guid Id() => Faker.Random.Guid();

    public static string FirstName() => Faker.Person.FirstName.Replace("'", "");

    public static string LastName() => Faker.Person.LastName.Replace("'", "");

    public static string Email() => $"taf.user.test.{FirstName()}_{LastName()}".ToLower() + Faker.Random.Number(0, 99) + "@gmail.com";

    public static string NameLength(int length) => Faker.Random.String2(length);

    public static string UniqueString() => Guid.NewGuid().ToString()[..8];

    public static string Fuzz() => Faker.Random.Int(111, 999).ToString();

    public static string Url()
    {
        var url = Faker.Internet.Url();
        if (url.Contains("http:")) url = url.Replace("http", "https");
        return url;
    }

    public static string CompanyName() => "Company " + Name();

    public static string News() => "News: " + Name();

    public static string Reply() => "Reply: " + Guid.NewGuid();

    private static Random Random() => new(Guid.NewGuid().GetHashCode());
}