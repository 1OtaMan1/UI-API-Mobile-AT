using Atata;
using UI.Atata;
using UI.Models;

namespace UI.Business.BaseApp.UserContribution;

using _ = AddCompanyPage;

[PageObjectDefinition("app-company-form")]
public class AddCompanyPage : CommonContributionPage<_>
{
    [FindByCss("input[formcontrolname='companyName']")]
    public CustomEditableTextField<_> CompanyNameInput { get; private set; }

    [FindByCss("input[formcontrolname='webSiteUrl']")]
    public CustomEditableTextField<_> CompanyUrlInput { get; private set; }

    [FindByCss("input[formcontrolname='logoUrl']")]
    public CustomEditableTextField<_> CompanyLogoUrlInput { get; private set; }

    [FindByCss("input[formcontrolname='ceoName']")]
    public CustomEditableTextField<_> CeoNameInput { get; private set; }

    public void FillCompanyData(CompanyUiModel companyModel)
    {
        CompanyNameInput.Wait(Until.Visible);
        CompanyNameInput.Set(companyModel.CompanyName);
        CompanyUrlInput.Set(companyModel.WebSiteUrl);
        CompanyLogoUrlInput.Set(companyModel.LogoUrl);
        CeoNameInput.Set(companyModel.CeoName);
        PrivacyCheckbox.ClickAndGo();
    }
}