namespace Core.EnvironmentSettings;

public static class CredentialsProvider
{
    private static readonly string Environment = ConfigurationManager.AppSettings.GetSection("Environment").Value;

    //  Tenant MAIL
    public static CredentialsStorage ContributorMailtrap()
    {
        return Environment switch
        {
            "stage" => ApiRoles.DevContributorMailtrap,
            "demo" => ApiRoles.DevContributorMailtrap,
            "prod" => ApiRoles.DevContributorMailtrap,
            _ => ApiRoles.DevContributorMailtrap
        };
    }

    public static CredentialsStorage SelfSignedContributorMailtrap()
    {
        return Environment switch
        {
            "stage" => ApiRoles.DevSelfSignedContributorMailtrap,
            "demo" => ApiRoles.DevSelfSignedContributorMailtrap,
            "prod" => ApiRoles.DevSelfSignedContributorMailtrap,
            _ => ApiRoles.DevSelfSignedContributorMailtrap
        };
    }

    public static CredentialsStorage DisabledContributorMailtrap()
    {
        return Environment switch
        {
            "stage" => ApiRoles.DevDisabledContributorMailtrap,
            "demo" => ApiRoles.DevDisabledContributorMailtrap,
            "prod" => ApiRoles.DevDisabledContributorMailtrap,
            _ => ApiRoles.DevDisabledContributorMailtrap
        };
    }

    public static CredentialsStorage Admin()
    {
        return Environment switch
        {
            "stage" => ApiRoles.DevAdmin,
            "demo" => ApiRoles.DevAdmin,
            "prod" => ApiRoles.DevAdmin,
            _ => ApiRoles.DevAdmin
        };
    }
}