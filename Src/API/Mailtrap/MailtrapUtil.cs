using API.Models.MailTrap;
using Core.EnvironmentSettings;
using Core.Helpers;
using MailKit.Net.Pop3;

namespace API.Mailtrap;

// Use MailtrapInbox from TenantApiUser instead
public static class MailtrapUtil
{
    private const string MailServerUrl = "pop3.mailtrap.io";
    private const int MailServerPort = 9950;
    private const int MaxMessageCount = 30;
    private const int PollingInterval = 3000;
    private const int Attempts = 10;

    private static readonly MailtrapApiClient MailtrapApiClient = new();

    public static List<MailtrapMessage> GetMessages(CredentialsStorage receiver, string subject, params string[] text)
    {
        var attempts = Attempts;
        var messages = new List<MailtrapMessage>();

        while (attempts-- > 0)
        {
            using (var client = new Pop3Client())
            {
                client.Connect(MailServerUrl, MailServerPort, false);
                client.Authenticate(receiver.MailServerLogin, receiver.MailServerPassword);

                var messageCount = client.GetMessageCount();

                var take = messageCount < MaxMessageCount
                    ? messageCount
                    : MaxMessageCount;

                var results = client.GetMessages(0, take);

                foreach (var result in results)
                {
                    if (result.Subject.Contains(subject) && text.All(data => result.HtmlBody.Contains(data)))
                    {
                        var message = new MailtrapMessage {Text = result.HtmlBody};
                        messages.Add(message);
                    }
                }
            }

            if (messages.Count > 0)
            {
                break;
            }

            Thread.Sleep(PollingInterval);
        }

        return messages;
    }

    public static MailtrapInboxApiModel CreateInbox(string name)
    {
        var inbox = MailtrapApiClient.CreateInbox(name);

        MailtrapApiClient.ToggleEmailForInbox(inbox.Id);
        inbox = MailtrapApiClient.GetInbox(inbox.Id);
        return inbox;
    }

    public static CredentialsStorage CreateMailboxCredentialsStorage(string? env = null)
    {
        var inbox = CreateInbox($"inbox_{Generator.UniqueString()}");
        var credentials = CredentialsStorageHelper.Create(inbox.Email);
        credentials.InboxId = inbox.Id;
        credentials.MailServerLogin = inbox.UserName;
        credentials.MailServerPassword = inbox.Password;
        return credentials;
    }

    public static void DeleteInbox(string inboxId)
    {
        MailtrapApiClient.DeleteInbox(inboxId);
    }

    public static void ClearFolder(CredentialsStorage user)
    {
        MailtrapApiClient.DeleteAllMessages(user.InboxId);
    }
}