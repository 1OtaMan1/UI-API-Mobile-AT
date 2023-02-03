using API.Helpers;
using API.Interfaces;
using API.Models.Admin.Core;
using API.Models.Admin.News;
using API.Models.Admin.News.Filters;
using Core.Extensions;
using Core.Helpers;
using RestSharp;

namespace API.Services.AdminNews;

public class AdminNewsService : IAdminNewsService
{
    private const string Path = "/api/AdminNews";

    private readonly IRequestManager _requestManager;

    public AdminNewsService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public Guid Create(AdminNewsAddApiModel newsModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/add",
            newsModel);

        return response.As<ResponseApiModel>().Id;
    }

    public Guid Update(AdminNewsAddApiModel newsModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/edit",
            newsModel);

        return response.As<ResponseApiModel>().Id;
    }

    public Guid Update(AdminNewsListItemApiModel newsListItemModel, string? title = null)
    {
        title ??= Generator.Text();

        var model = new AdminNewsApiModelBuilder(newsListItemModel.CompanyId).SetId(newsListItemModel.Id)
            .SetCreationDate(newsListItemModel.CreatedDate)
            .SetTitle(title)
            .SetConfirmationStatus(newsListItemModel.IsConfirmed)
            .SetVerificationStatus(newsListItemModel.IsVerified).Build();

        return Update(model);
    }

    public void Delete(Guid newsId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete",
            newsId);
    }

    public IEnumerable<AdminNewsListItemApiModel> GetList(AdminNewsFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/list",
            filterModel);

        return response.As<IEnumerable<AdminNewsListItemApiModel>>();
    }

    public IEnumerable<AdminNewsListItemApiModel> GetList(string? searchQuery = null)
    {
        var filterModel = new AdminNewsFilterApiModel { SearchQuery = searchQuery };
        return GetList(filterModel);
    }

    public IEnumerable<AdminNewsListItemApiModel> GetCompanyNews(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + $"/list/{companyId}");

        return response.As<IEnumerable<AdminNewsListItemApiModel>>();
    }

    public IEnumerable<AdminNewsListItemApiModel> GetUserNews(Guid userId)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + $"/list/{userId}/user");

        return response.As<IEnumerable<AdminNewsListItemApiModel>>();
    }

    public NewsCountApiModel GetCount(AdminNewsFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/count",
            filterModel);

        return response.As<NewsCountApiModel>();
    }

    public NewsCountApiModel GetCount(string? searchQuery = null)
    {
        var filterModel = new AdminNewsFilterApiModel { SearchQuery = searchQuery };
        return GetCount(filterModel);
    }

    public int GetCompanyNewsCount(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/count/{companyId}");

        return response.As<int>();
    }

    public int GetUserNewsCount(Guid userId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/count/{userId}/user");

        return response.As<int>();
    }

    public AdminNewsListItemApiModel Get(Guid newsId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/{newsId}");

        return response.As<AdminNewsListItemApiModel>();
    }

    public void ChangeVerificationStatus(Guid newsId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isverified/{newsId}");
    }

    public void ChangeConfirmationStatus(Guid newsId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isconfirmed/{newsId}");
    }
}