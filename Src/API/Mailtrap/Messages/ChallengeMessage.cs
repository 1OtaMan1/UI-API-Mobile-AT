namespace API.Mailtrap.Messages;

public class ChallengeMessage : HtmlMessageBase
{
    private string DescriptionXPath => "(//table[@id='bodyTable']//div//td)[2]";
    private string QuoteXPath => DescriptionXPath + "//table";
    private string ChallengeNameXPath => DescriptionXPath + "//a";

    public ChallengeMessage(string message) : base(message) { }

    public string Description => FormatSpaces(ExtractText(DescriptionXPath));

    public string Quote => ExtractText(QuoteXPath);

    public string ChallengeName => ExtractText(ChallengeNameXPath);

}