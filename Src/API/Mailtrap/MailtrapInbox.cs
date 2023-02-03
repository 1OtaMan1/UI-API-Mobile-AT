using API.Exceptions;
using API.Interfaces;
using API.Mailtrap.Messages;
using Core.EnvironmentSettings;
using Core.Utils;
using MailKit.Net.Pop3;
using MimeKit;

namespace API.Mailtrap;

public class MailtrapInbox : IMailBox
{
    private const string MailServerUrl = "pop3.mailtrap.io";
    private const int MailServerPort = 9950;
    private const int MaxMessageCount = 30;
    private const int PollingInterval = 3000;

    private readonly CredentialsStorage _credentials;

    public MailtrapInbox(CredentialsStorage credentials)
    {
        _credentials = credentials;
    }

    public HtmlMessageBase GetHtmlMessage(string subject, string body, int attempts = 10)
        => new(GetMessage(subject, body, attempts));

    public ChallengeMessage GetChallengeMessage(string subject, string body, int attempts = 10)
        => new(GetMessage(subject, body, attempts));

    public string GetMessage(string subject, string body, int attempts = 10)
    {
        while (attempts-- > 0)
        {
            using (var client = new Pop3Client())
            {
                client.Connect(MailServerUrl, MailServerPort, false);
                client.Authenticate(_credentials.MailServerLogin, _credentials.MailServerPassword);

                var messageCount = client.GetMessageCount();

                var take = messageCount < MaxMessageCount
                    ? messageCount
                    : MaxMessageCount;

                if (take == 0)
                {
                    Throttle();
                    continue;
                }

                var results = new List<MimeMessage>();

                Retry.Exponential<Pop3CommandException>
                    (6, () => results = client.GetMessages(0, take).ToList());

                foreach (var result in results)
                {
                    if (result.Subject.Contains(subject) && result.HtmlBody.Contains(body))
                    {
                        return result.HtmlBody;
                    }
                }
            }

            Throttle();
        }

        throw new ExpectedMessagesException($"Failed to find \"{subject}\" message");
    }

    private static void Throttle() => Thread.Sleep(PollingInterval);
}