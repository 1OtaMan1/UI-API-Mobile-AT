using Atata;
using UI.Business.BaseApp.UserContribution;

namespace UI.Business.BaseApp.Home;

[PageObjectDefinition("app-home-page")]
public class CommonHomePage<TOwner> : MainPage<TOwner>
    where TOwner : CommonHomePage<TOwner>
{
    [FindByClass("empty-state-container")]
    public Text<TOwner> EmptyState { get; private set; }

    [FindByClass("description-container")]
    public Text<TOwner> AppDescription { get; private set; }

    public InfoContainer InitiativeMainInfo { get; private set; }

    [ControlDefinition("div", ContainingClass = "main-info-container")]
    public class InfoContainer : Control<TOwner>
    {
        [FindByClass("title")]
        public Text<TOwner> Title { get; private set; }

        [FindByXPath("p[contains(text(), 'Shares')]/parent::div[@class='share-container']/span[@class='counter']")]
        public Text<TOwner> SharesQty { get; private set; }

        [FindByXPath("p[contains(text(), 'supporters')]/parent::div[@class='share-container']/span[@class='counter']")]
        public Text<TOwner> SupportersQty { get; private set; }

        [FindByXPath("span[normalize-space(text())='Petition all']/parent::button")]
        public Button<PetitionAllPage, TOwner> PetitionAllButton { get; private set; }
    }

    public TabsContainer Tabs { get; private set; }

    [ControlDefinition("div", ContainingClass = "mat-tab-label-container")]
    public class TabsContainer : Control<TOwner>
    {
        [FindById("mat-tab-label-0-0")]
        public Clickable<BrandsTab, TOwner> Brands { get; private set; }

        [FindById("mat-tab-label-0-1")]
        public Clickable<NewsTab, TOwner> News { get; private set; }
    }

    [FindByCss("div.search-container")]
    public SearchInput<TOwner> SearchInput { get; private set; }

    public BrandsTab SwitchToBrandsTab()
    {
        Tabs.Brands.Wait(Until.Visible);
        return Tabs.Brands.ClickAndGo();
    }

    public NewsTab SwitchToNewsTab()
    {
        Tabs.News.Wait(Until.Visible);
        return Tabs.News.ClickAndGo();
    }

    public PetitionAllPage StartPetitionAllSign()
    {
        InitiativeMainInfo.PetitionAllButton.Wait(Until.Visible);
        var petitionAllPage = InitiativeMainInfo.PetitionAllButton.ClickAndGo();
        Go.ToNextWindow(petitionAllPage);
        return petitionAllPage.SubmitButton.Wait(Until.Visible);
    }
}