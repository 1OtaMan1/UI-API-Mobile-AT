using API.Interfaces;
using API.Models.Admin.Company;
using API.Models.Admin.Company.Filters;
using API.Models.Admin.Core;
using API.Models.Core;
using Core.Extensions;
using RestSharp;

namespace API.Services.AdminCompany;

public class AdminCompanyService : IAdminCompanyService
{
    private const string Path = "/api/AdminCompany";

    private readonly IRequestManager _requestManager;

    public AdminCompanyService(IRequestManager requestManager)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        _requestManager = requestManager ?? throw new NullReferenceException(nameof(requestManager));
#pragma warning restore CA2201 // Do not raise reserved exception types
    }

    public Guid Create(AdminCompanyAddApiModel companyModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/add",
            companyModel);

        return response.As<ResponseApiModel>().Id;
    }

    public Guid Update(AdminCompanyAddApiModel companyModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/edit",
            companyModel);

        return response.As<ResponseApiModel>().Id;
    }

    public void Delete(Guid companyId)
    {
        _requestManager.SendRequest(
            Method.DELETE,
            Path + "/delete",
            companyId);
    }

    public IEnumerable<AdminCompanyListItemApiModel> GetList(AdminCompanyFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/list",
            filterModel);

        return response.As<IEnumerable<AdminCompanyListItemApiModel>>();
    }

    public IEnumerable<AdminCompanyListItemApiModel> GetList(string? searchQuery = null)
    {
        var filterModel = new AdminCompanyFilterApiModel { SearchQuery = searchQuery };
        return GetList(filterModel);
    }

    public IEnumerable<AdminCompanyListItemApiModel> GetUserCompanies(Guid userId)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + $"/list/{userId}");

        return response.As<IEnumerable<AdminCompanyListItemApiModel>>();
    }

    public IEnumerable<IdNamePairApiModel> GetNamesWithIds()
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + "/names");

        return response.As<IEnumerable<IdNamePairApiModel>>();
    }

    public IdNamePairApiModel GetNameWithId(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/name/{companyId}");

        return response.As<IdNamePairApiModel>();
    }

    public CompanyCountApiModel GetCount(AdminCompanyFilterApiModel filterModel)
    {
        var response = _requestManager.SendRequest(
            Method.POST,
            Path + "/count",
            filterModel);

        return response.As<CompanyCountApiModel>();
    }

    public CompanyCountApiModel GetCount(string? searchQuery = null)
    {
        var filterModel = new AdminCompanyFilterApiModel { SearchQuery = searchQuery };
        return GetCount(filterModel);
    }

    public int GetUserCompaniesCount(Guid userId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/count/{userId}");

        return response.As<int>();
    }

    public AdminCompanyListItemApiModel Get(Guid companyId)
    {
        var response = _requestManager.SendRequest(
            Method.GET,
            Path + $"/{companyId}");

        return response.As<AdminCompanyListItemApiModel>();
    }

    public void ChangeVerificationStatus(Guid companyId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isverified/{companyId}");
    }

    public void ChangeConfirmationStatus(Guid companyId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isconfirmed/{companyId}");
    }

    public void ChangeActivationStatus(Guid companyId)
    {
        _requestManager.SendRequest(
            Method.PUT,
            Path + $"/isworking/{companyId}");
    }
}