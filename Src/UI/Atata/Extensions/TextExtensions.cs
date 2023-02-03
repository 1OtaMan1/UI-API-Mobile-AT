using Atata;

namespace UI.Atata.Extensions;

public static class TextExtensions
{
    public static TOwner ShouldBe<TOwner>(this Field<string, TOwner> text, string expectedText)
        where TOwner : PageObject<TOwner>
    {
        return text.Should.WithRetry.ContainIgnoringCase(expectedText);
    }
}