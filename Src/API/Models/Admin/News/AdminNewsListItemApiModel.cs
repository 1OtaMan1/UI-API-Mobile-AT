namespace API.Models.Admin.News;

public class AdminNewsListItemApiModel
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public string Title { get; set; }

    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsVerified { get; set; }

    public Guid CreatedBy { get; set; }

    public string CreatorEmail { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}