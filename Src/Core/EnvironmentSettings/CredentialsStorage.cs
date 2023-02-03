using OpenQA.Selenium;

namespace Core.EnvironmentSettings;

public class CredentialsStorage
{
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => LastName + " " + FirstName;

    public bool IsDisabled { get; set; } = false;

    public string InboxId { get; set; }

    public string MailServerLogin { get; set; }

    public string MailServerPassword { get; set; }

    public string EnvironmentType { get; set; }

    public List<Cookie> Cookies { get; set; }

    public string? Token { get; set; }

    public override string ToString() => Email;
}