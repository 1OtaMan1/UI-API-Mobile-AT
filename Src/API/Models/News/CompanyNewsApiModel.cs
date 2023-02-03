namespace API.Models.News;

public class CompanyNewsApiModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string WebSiteUrl { get; set; }

    public string LogoUrl { get; set; }

    public bool IsMoreNewsAvailable { get; set; }

    public int SharesCount { get; set; }

    public int LikesCount { get; set; }

    public DateTime LastNewsDate { get; set; }

    public List<NewsApiModel> News { get; set; } = new();
}