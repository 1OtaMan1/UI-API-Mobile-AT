using API.Models.News;

namespace API.Services.News;

public interface INewsService
{
    IEnumerable<CompanyNewsApiModel> GetCompaniesWithNewsList(NewsFilterApiModel filterModel);

    IEnumerable<CompanyNewsApiModel> GetCompaniesWithNewsList(string? searchQuery = null);

    CompanyNewsApiModel GetCompanyNews(Guid companyId);

    void Create(NewsCreationApiModel newsModel);

    void ConfirmNews(IEnumerable<Guid> newsIds);

    IEnumerable<NewsApiModel> GetCompanyNewsShortList(Guid companyId);
}