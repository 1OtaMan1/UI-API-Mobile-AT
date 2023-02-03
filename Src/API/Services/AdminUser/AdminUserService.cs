using API.Interfaces;
using API.Models.Admin.Core;
using API.Models.Admin.User;
using API.Models.Admin.User.Filters;
using Core.Extensions;
using RestSharp;

namespace API.Services.AdminUser;

public class AdminUserService : IAdminUserService
{
    private const string Path = "/api/AdminUser";

    private readonly IRequestManager _requestManager;

    public AdminUserService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public Guid Create(AdminUserAddApiModel userModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/add",
            userModel);

        return response.As<ResponseApiModel>().Id;
    }

    public Guid Update(AdminUserAddApiModel userModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/edit",
            userModel);

        return response.As<ResponseApiModel>().Id;
    }

    public void Delete(Guid userId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete",
            userId);
    }

    public void DeleteUserNews(Guid userId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete/news",
            userId);
    }

    public void DeleteUserCompanies(Guid userId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete/company",
            userId);
    }

    public IEnumerable<AdminUserListItemApiModel> GetList(AdminUserFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/list",
            filterModel);

        return response.As<IEnumerable<AdminUserListItemApiModel>>();
    }

    public IEnumerable<AdminUserListItemApiModel> GetList(string? searchQuery = null)
    {
        var filterModel = new AdminUserFilterApiModel { SearchQuery = searchQuery };
        return GetList(filterModel);
    }

    public int GetCount(AdminUserFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/count",
            filterModel);

        return response.As<int>();
    }

    public int GetCount(string? searchQuery = null)
    {
        var filterModel = new AdminUserFilterApiModel { SearchQuery = searchQuery };
        return GetCount(filterModel);
    }

    public AdminUserDetailsApiModel Get(Guid userId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/{userId}");

        return response.As<AdminUserDetailsApiModel>();
    }

    public void ChangeActivationStatus(Guid userId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isdisabled/{userId}");
    }
}