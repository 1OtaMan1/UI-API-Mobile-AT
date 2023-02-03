using API.Models.Admin.User;
using API.Models.Admin.User.Filters;

namespace API.Services.AdminUser;

public interface IAdminUserService
{
    Guid Create(AdminUserAddApiModel userModel);

    Guid Update(AdminUserAddApiModel userModel);

    void Delete(Guid userId);

    void DeleteUserNews(Guid userId);

    void DeleteUserCompanies(Guid userId);

    IEnumerable<AdminUserListItemApiModel> GetList(AdminUserFilterApiModel filterModel);

    IEnumerable<AdminUserListItemApiModel> GetList(string searchQuery = null);

    int GetCount(AdminUserFilterApiModel filterModel);

    int GetCount(string? searchQuery = null);

    AdminUserDetailsApiModel Get(Guid userId);

    void ChangeActivationStatus(Guid userId);
}