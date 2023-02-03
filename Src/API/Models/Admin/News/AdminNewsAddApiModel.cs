namespace API.Models.Admin.News;

public class AdminNewsAddApiModel
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string Title { get; set; }

    public string Url { get; set; }

    public DateTimeOffset Date { get; set; }

    public bool IsVerified { get; set; }

    public bool IsConfirmed { get; set; }
}