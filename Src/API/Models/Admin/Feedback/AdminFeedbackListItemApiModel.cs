namespace API.Models.Admin.Feedback;

public class AdminFeedbackListItemApiModel
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string FeedbackMessage { get; set; }

    public bool IsReplied { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}