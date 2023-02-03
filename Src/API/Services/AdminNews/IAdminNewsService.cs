using API.Models.Admin.News;
using API.Models.Admin.News.Filters;

namespace API.Services.AdminNews;

public interface IAdminNewsService
{
    Guid Create(AdminNewsAddApiModel newsModel);

    Guid Update(AdminNewsAddApiModel newsModel);

    Guid Update(AdminNewsListItemApiModel newsListItemModel, string? title = null);

    void Delete(Guid newsId);

    IEnumerable<AdminNewsListItemApiModel> GetList(AdminNewsFilterApiModel filterModel);

    IEnumerable<AdminNewsListItemApiModel> GetList(string? searchQuery = null);

    IEnumerable<AdminNewsListItemApiModel> GetCompanyNews(Guid companyId);

    IEnumerable<AdminNewsListItemApiModel> GetUserNews(Guid userId);

    NewsCountApiModel GetCount(AdminNewsFilterApiModel filterModel);

    NewsCountApiModel GetCount(string? searchQuery = null);

    int GetCompanyNewsCount(Guid companyId);

    int GetUserNewsCount(Guid userId);

    AdminNewsListItemApiModel Get(Guid newsId);

    void ChangeVerificationStatus(Guid newsId);

    void ChangeConfirmationStatus(Guid newsId);
}