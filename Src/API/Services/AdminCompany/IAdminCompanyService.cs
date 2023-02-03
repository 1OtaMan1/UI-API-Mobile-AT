using API.Models.Admin.Company;
using API.Models.Admin.Company.Filters;
using API.Models.Core;

namespace API.Services.AdminCompany;

public interface IAdminCompanyService
{
    Guid Create(AdminCompanyAddApiModel companyModel);

    Guid Update(AdminCompanyAddApiModel companyModel);

    void Delete(Guid companyId);

    IEnumerable<AdminCompanyListItemApiModel> GetList(AdminCompanyFilterApiModel filterModel);

    IEnumerable<AdminCompanyListItemApiModel> GetList(string? searchQuery = null);

    IEnumerable<AdminCompanyListItemApiModel> GetUserCompanies(Guid userId);

    IEnumerable<IdNamePairApiModel> GetNamesWithIds();

    IdNamePairApiModel GetNameWithId(Guid companyId);

    CompanyCountApiModel GetCount(AdminCompanyFilterApiModel filterModel);

    CompanyCountApiModel GetCount(string? searchQuery = null);

    int GetUserCompaniesCount(Guid userId);

    AdminCompanyListItemApiModel Get(Guid companyId);

    void ChangeVerificationStatus(Guid companyId);

    void ChangeConfirmationStatus(Guid companyId);

    void ChangeActivationStatus(Guid companyId);
}