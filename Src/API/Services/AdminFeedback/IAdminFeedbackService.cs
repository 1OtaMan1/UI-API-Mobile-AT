using API.Models.Admin.Feedback;
using API.Models.Admin.Feedback.Filter;

namespace API.Services.AdminFeedback;

public interface IAdminFeedbackService
{
    Guid Reply(AdminFeedbackReplyAddApiModel feedbackModel);

    void Delete(Guid feedbackId);

    IEnumerable<AdminFeedbackListItemApiModel> GetList(AdminFeedbackFilterApiModel filterModel);

    IEnumerable<AdminFeedbackListItemApiModel> GetList(string? searchQuery = null);

    FeedbackCountApiModel GetCount(AdminFeedbackFilterApiModel filterModel);

    FeedbackCountApiModel GetCount(string? searchQuery = null);

    int GetRepliesCount(Guid feedbackId);

    AdminFeedbackListItemApiModel Get(Guid feedbackId);

    IEnumerable<AdminFeedbackReplyListItemApiModel> GetRepliesList(Guid feedbackId);
}