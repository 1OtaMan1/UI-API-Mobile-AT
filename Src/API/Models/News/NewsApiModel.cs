namespace API.Models.News;

public class NewsApiModel
{
    public string Url { get; set; }

    public string Title { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}