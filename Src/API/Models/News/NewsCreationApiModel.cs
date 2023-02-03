namespace API.Models.News;

public class NewsCreationApiModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string CompanyWebSiteUrl { get; set; }

    public string CompanyName { get; set; }

    public IEnumerable<string> NewsUrls { get; set; }

    public string RecaptchaToken { get; set; }
}