namespace API.Models;

public class ConfigApiModel
{
    public string ApiName { get; set; }

    public string StsBaseUrl { get; set; }

    public string SaaSAdminBaseUrl { get; set; }

    public string RootBaseUrl { get; set; }

    public string HelpDeskEmailAlias { get; set; }

    public string FeedbackEmailAlias { get; set; }

    public string PrivacyPolicyUrl { get; set; }

    public string TermsAndConditionsUrl { get; set; }

    public string IosAppUrl { get; set; }

    public string AndroidAppUrl { get; set; }
}