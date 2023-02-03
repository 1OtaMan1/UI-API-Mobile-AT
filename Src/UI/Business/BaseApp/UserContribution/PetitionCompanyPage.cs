using Atata;

namespace UI.Business.BaseApp.UserContribution;

using _ = PetitionCompanyPage;

[PageObjectDefinition("app-petition-form")]
public class PetitionCompanyPage : CommonContributionPage<_>
{
    public CompanyInfoContainer CompanyInfo { get; private set; }

    [ControlDefinition("div", ContainingClass = "company-block")]
    public class CompanyInfoContainer : Control<_>
    {
        [FindByClass("company-name")]
        public Text<_> Name { get; private set; }

        [FindByClass("company-image")]
        public Control<_> Logo { get; private set; }

        [FindByCss("p.label")]
        public Text<_> SignAsEmploeeLabel { get; private set; }
    }
}