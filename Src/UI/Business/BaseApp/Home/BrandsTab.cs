using Atata;
using UI.Business.BaseApp.Home.Common.Sections;
using UI.Business.BaseApp.UserContribution;

namespace UI.Business.BaseApp.Home;

using _ = BrandsTab;

public class BrandsTab : CommonHomePage<_>
{
    [FindByXPath("span[normalize-space(text())='Submit a company']/parent::button")]
    public Button<AddCompanyPage, _> AddCompanyButton { get; private set; }

    public ControlList<CompanyCard, _> CompaniesCards { get; private set; }

    [ControlDefinition("app-company-card")]
    public class CompanyCard : Control<_>
    {
        [FindByClass("news-shared")]
        public Control<_> Highlight { get; private set; }

        [FindByClass("socials-badge")]
        public Clickable<_, _> ShareIcon { get; private set; }

        [ControlDefinition("ancestor::body//div", ContainingClass = "models-type-popover")]
        public SocialMedias<_> SocialMediasTooltip { get; private set; }

        [FindByClass("mat-card-image")]
        public Control<_> Logo { get; private set; }

        [FindByClass("card-title")]
        public Text<_> CompanyName { get; private set; }

        [FindByClass("card-description")]
        public Text<_> CEOName { get; private set; }

        [FindByCss("div.counter-button span.counter")]
        public Text<_> SignedPetitionsQty { get; private set; }

        [FindByXPath("span[normalize-space(text())='Employee petition']/parent::button")]
        public Button<PetitionCompanyPage, _> AddPetitionButton { get; private set; }

        public PetitionCompanyPage StartPetitionSign()
        {
            AddPetitionButton.Wait(Until.Visible);
            var petitionCompanyPage = AddPetitionButton.ClickAndGo();
            Go.ToNextWindow(petitionCompanyPage);
            return petitionCompanyPage.SubmitButton.Wait(Until.Visible);
        }
    }

    public CompanyCard GetCompany(string companyName) => CompaniesCards[el => el.CompanyName.Value.ToLower().Contains(companyName.ToLower())];

    public AddCompanyPage StartCompanyAdd()
    {
        AddCompanyButton.Wait(Until.Visible);
        var addCompanyPage = AddCompanyButton.ClickAndGo();
        Go.ToNextWindow(addCompanyPage);
        return addCompanyPage.CompanyNameInput.Wait(Until.Visible);
    }
}