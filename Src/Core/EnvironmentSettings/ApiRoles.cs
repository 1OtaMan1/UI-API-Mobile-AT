namespace Core.EnvironmentSettings;

public static class ApiRoles
{
    //  Users with mailtrap:
    public static CredentialsStorage ContributorMailtrap { get; set; } = CredentialsProvider.ContributorMailtrap();

    public static CredentialsStorage SelfSignedContributorMailtrap { get; set; } = CredentialsProvider.SelfSignedContributorMailtrap();

    public static CredentialsStorage DisabledContributorMailtrap { get; set; } = CredentialsProvider.DisabledContributorMailtrap();

    public static CredentialsStorage Admin { get; set; } = CredentialsProvider.Admin();

    //  Tenant users with mailtrap:
    public static CredentialsStorage DevContributorMailtrap =>
        new()
        {
            Id = new Guid("d1dd9152-0bc4-478d-01d1-08da17b733a3"),
            FirstName = "Контрибютор",
            LastName = "Соломон",
            Email = "6bd5f45cc6-b70ddb@inbox.mailtrap.io",
            EnvironmentType = "testtenant",
            InboxId = "1687895",
            MailServerLogin = "95efc824eb2e41",
            MailServerPassword = "b43361392852ed",
            IsDisabled = false
        };

    public static CredentialsStorage DevSelfSignedContributorMailtrap =>
        new()
        {
            Id = new Guid("41c2a4f4-140f-41e0-8ac3-b20b5d959f0d"),
            FirstName = "Самостійний",
            LastName = "Соломон",
            Email = "933d48f113-827488@inbox.mailtrap.io",
            EnvironmentType = "testtenant",
            InboxId = "1692375",
            MailServerLogin = "22fab62ac962cc",
            MailServerPassword = "6d0338569bcfb6",
            IsDisabled = false
        };

    public static CredentialsStorage DevDisabledContributorMailtrap =>
        new()
        {
            Id = new Guid("02668fba-22ba-4ec9-630a-08da195a4598"),
            FirstName = "Дізейблед",
            LastName = "Соломон",
            Email = "ebe8e547a0-5e4307@inbox.mailtrap.io",
            EnvironmentType = "testtenant",
            InboxId = "1694172",
            MailServerLogin = "a7f56350dba87b",
            MailServerPassword = "f7b0685aa0b7af",
            IsDisabled = true
        };

    public static CredentialsStorage DevAdmin =>
        new()
        {
            Id = new Guid("48d33d91-79ef-49e5-9611-43dc56ff9a4b"),
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.admin",
            Login = "admin@stopbusinesswithrussia.org",
            Password = "AdminStrongPassword12345!",
            IsDisabled = false
        };
}