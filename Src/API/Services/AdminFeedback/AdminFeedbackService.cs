using API.Interfaces;
using API.Models.Admin.Core;
using API.Models.Admin.Feedback;
using API.Models.Admin.Feedback.Filter;
using Core.Extensions;
using RestSharp;

namespace API.Services.AdminFeedback;

public class AdminFeedbackService : IAdminFeedbackService
{
    private const string Path = "/api/AdminFeedback";

    private readonly IRequestManager _requestManager;

    public AdminFeedbackService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public Guid Reply(AdminFeedbackReplyAddApiModel feedbackModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/add",
            feedbackModel);

        return response.As<ResponseApiModel>().Id;
    }

    public void Delete(Guid feedbackId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete");
    }

    public IEnumerable<AdminFeedbackListItemApiModel> GetList(AdminFeedbackFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/list",
            filterModel);

        return response.As<IEnumerable<AdminFeedbackListItemApiModel>>();
    }

    public IEnumerable<AdminFeedbackListItemApiModel> GetList(string? searchQuery = null)
    {
        var filterModel = new AdminFeedbackFilterApiModel { SearchQuery = searchQuery };
        return GetList(filterModel);
    }

    public FeedbackCountApiModel GetCount(AdminFeedbackFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/count",
            filterModel);

        return response.As<FeedbackCountApiModel>();
    }

    public FeedbackCountApiModel GetCount(string? searchQuery = null)
    {
        var filterModel = new AdminFeedbackFilterApiModel { SearchQuery = searchQuery };
        return GetCount(filterModel);
    }

    public int GetRepliesCount(Guid feedbackId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/count/{feedbackId}");

        return response.As<int>();
    }

    public AdminFeedbackListItemApiModel Get(Guid feedbackId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/{feedbackId}");

        return response.As<AdminFeedbackListItemApiModel>();
    }

    public IEnumerable<AdminFeedbackReplyListItemApiModel> GetRepliesList(Guid feedbackId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/replies/{feedbackId}");

        return response.As<IEnumerable<AdminFeedbackReplyListItemApiModel>>();
    }
}