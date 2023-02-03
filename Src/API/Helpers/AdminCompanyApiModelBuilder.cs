using API.Models.Admin.Company;
using Core.Helpers;

namespace API.Helpers;

public class AdminCompanyApiModelBuilder
{
    private readonly AdminCompanyAddApiModel _model;

    public AdminCompanyApiModelBuilder()
    {
        _model = new AdminCompanyAddApiModel
        {
            CompanyName = Generator.CompanyName(),
            WebSiteUrl = Generator.Url(),
            LogoUrl = Generator.Url(),
            CeoName = Generator.FirstName() + " " + Generator.LastName(),
            IsWorking = false,
            IsVerified = false,
            IsConfirmed = false
        };
    }

    public AdminCompanyAddApiModel Build() => _model;

    public AdminCompanyApiModelBuilder SetCompanyName(string companyName)
    {
        _model.CompanyName = companyName;
        return this;
    }

    public AdminCompanyApiModelBuilder SetVerificationStatus(bool isVerified)
    {
        _model.IsVerified = isVerified;
        return this;
    }

    public AdminCompanyApiModelBuilder SetConfirmationStatus(bool isConfirmed)
    {
        _model.IsConfirmed = isConfirmed;
        return this;
    }
}