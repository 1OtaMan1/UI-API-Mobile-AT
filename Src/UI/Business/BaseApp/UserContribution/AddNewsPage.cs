using Atata;
using UI.Atata;
using UI.Models;

namespace UI.Business.BaseApp.UserContribution;

using _ = AddNewsPage;

[PageObjectDefinition("app-company-form")]
public class AddNewsPage : CommonContributionPage<_>
{
    [FindByCss("input[formcontrolname='companyName']")]
    public CustomEditableTextField<_> CompanyNameInput { get; private set; }

    [FindByCss("input[formcontrolname='companyWebSiteUrl']")]
    public CustomEditableTextField<_> CompanyUrlInput { get; private set; }

    [FindById("mat-input-5")]
    public CustomEditableTextField<_> FirstNewsUrlInput { get; private set; }

    public void FillNewsData(NewsUiModel newsModel)
    {
        CompanyNameInput.Wait(Until.Visible);
        CompanyNameInput.Set(newsModel.CompanyName);
        CompanyUrlInput.Set(newsModel.CompanyUrl);
        FirstNewsUrlInput.Set(newsModel.NewsUrl.First());
        PrivacyCheckbox.ClickAndGo();
    }
}