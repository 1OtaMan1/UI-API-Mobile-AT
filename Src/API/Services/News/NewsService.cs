using API.Interfaces;
using API.Models.Core;
using API.Models.News;
using Core.Extensions;
using RestSharp;

namespace API.Services.News;

public class NewsService : INewsService
{
    private const string Path = "/api/News";

    private readonly IRequestManager _requestManager;

    public NewsService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public IEnumerable<CompanyNewsApiModel> GetCompaniesWithNewsList(NewsFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/list",
            filterModel);

        return response.As<VirtualizationResult<CompanyNewsApiModel>>().Items;
    }

    public IEnumerable<CompanyNewsApiModel> GetCompaniesWithNewsList(string? searchQuery = null)
    {
        var filterModel = new NewsFilterApiModel { SearchQuery = searchQuery };
        return GetCompaniesWithNewsList(filterModel);
    }

    public CompanyNewsApiModel GetCompanyNews(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/{companyId}");

        return response.As<CompanyNewsApiModel>();
    }

    public void Create(NewsCreationApiModel newsModel)
    {
        _requestManager.SendRequest(
            Method.POST,
            Path,
            newsModel);
    }

    public void ConfirmNews(IEnumerable<Guid> newsIds)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + "/confirm",
            newsIds);
    }

    public IEnumerable<NewsApiModel> GetCompanyNewsShortList(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/company/{companyId}");

        return response.As<IEnumerable<NewsApiModel>>();
    }
}