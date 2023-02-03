using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace API.Mailtrap.Messages;

public class HtmlMessageBase
{
    protected virtual string EventXPath => "(//table[@id='bodyTable']//div)[1]";
    protected virtual string GreetingXPath => "(//table[@id='bodyTable']//div)[2]";
    protected virtual string NotificationPreferencesXPath => "//span[contains(text(), 'Notifications preferences')]/parent::a";
    protected virtual string ContactHelpdeskXPath => "//span[contains(text(), 'Contact helpdesk')]/parent::a";
    protected virtual string EntityLinkXPath(string name) => $"//span[contains(text(),'{ name }')]/parent::a";

    private const string Expression = "<a.+href=\\\"(.+)\"";

    protected HtmlDocument _htmlDoc;

    public HtmlMessageBase(string message)
    {
        _htmlDoc = new HtmlDocument();
        _htmlDoc.LoadHtml(message);
    }

    public virtual string Event => ExtractText(EventXPath).Trim();

    public virtual string Greeting => ExtractText(GreetingXPath);

    public virtual string NotificationsPreferences => ExtractLink(NotificationPreferencesXPath);

    public virtual string ContactHelpdesk => ExtractLink(ContactHelpdeskXPath);

    public virtual string Body => _htmlDoc.DocumentNode.InnerHtml;

    public virtual string EntityLink(string name) => ExtractLink(EntityLinkXPath(name)).Replace("amp;", "");

    public string ConfirmationLink => ExtractLink("//a[contains(@href, 'confirm')]").Replace("amp;", "");

    public string RegisterLink => ExtractLink("//a[contains(@href, 'confirmregistration')]").Replace("amp;", "");

    public string ResetPasswordLink => ExtractLink("//a[contains(@href, 'resetpassword')]").Replace("amp;", "");

    protected string ExtractText(string xpath) => _htmlDoc.DocumentNode.SelectSingleNode(xpath).InnerText.Trim();

    protected string ExtractLink(string xpath) => _htmlDoc.DocumentNode.SelectSingleNode(xpath).GetAttributeValue("href", String.Empty);

    protected string FormatSpaces(string text) => Regex.Replace(text, @"\s+", " ");

    public static string ResetPasswordUrl(MailtrapMessage message) =>
        Regex.Match(message.Text, Expression).Groups[1].Value.Replace("amp;", "").Replace("\" target=\"_blank", "");
}