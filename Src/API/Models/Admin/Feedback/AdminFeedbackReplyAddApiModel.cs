namespace API.Models.Admin.Feedback;

public class AdminFeedbackReplyAddApiModel
{
    public AdminFeedbackReplyAddApiModel()
    {
        FeedbackId = Guid.Empty;
        ReplyMessage = string.Empty;
    }

    public string Email { get; set; }

    public Guid FeedbackId { get; set; }

    public string ReplyMessage { get; set; }
}