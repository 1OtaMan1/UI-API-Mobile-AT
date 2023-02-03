using API.Mailtrap.Messages;

namespace API.Interfaces;

public interface IMailBox
{
    string GetMessage(string subject, string body, int attempts = 10);

    HtmlMessageBase GetHtmlMessage(string subject, string body, int attempts = 10);

    ChallengeMessage GetChallengeMessage(string subject, string body, int attempts = 10);
}