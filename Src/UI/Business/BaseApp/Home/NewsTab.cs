using Atata;
using Core.Utils;
using UI.Business.BaseApp.Home.Common.Sections;
using UI.Business.BaseApp.UserContribution;
using static Core.Constants.Sizes;

namespace UI.Business.BaseApp.Home;

using _ = NewsTab;

[PageObjectDefinition("app-news-tab")]
public class NewsTab : CommonHomePage<_>
{
    [FindByXPath("span[normalize-space(text())='Add news about company']/parent::button")]
    public Button<AddNewsPage, _> AddCompanyNewsButton { get; private set; }

    public ControlList<BrandNews, _> BrandsNews { get; private set; }

    [ControlDefinition("app-company-news-card")]
    public class BrandNews : Control<_>
    {
        [FindByXPath("ancestor::div[contains(@class, 'news')]/div[@class='news-date']")]
        public Text<_> NewsDate { get; private set; }

        [FindByCss("div.company-container .title")]
        public Text<_> BrandName { get; private set; }

        [FindByClass("company-image")]
        public Control<_> BrandImage { get; private set; }

        [FindByCss("mat-icon.like")]
        public Clickable<_, _> LikeIcon { get; private set; }

        [FindByXPath("*[contains(@class, 'like')]/following-sibling::span")]
        public Text<_> LikeCounter { get; private set; }

        [FindByClass("icon share")]
        public Clickable<_, _> ShareIcon { get; private set; }

        [FindByXPath("*[contains(@class, 'share')]/following-sibling::span")]
        public Text<_> ShareCounter { get; private set; }

        [ControlDefinition("ancestor::body//div", ContainingClass = "models-type-popover")]
        public SocialMedias<_> SocialMediasTooltip { get; private set; }

        [ControlDefinition("div", ContainingClass = "latest-news")]
        public NewsContainer LatestNews { get; private set; }

        public OtherNewsContainer OtherNews { get; private set; }

        [ControlDefinition("mat-accordion")]
        public class OtherNewsContainer : Control<_>
        {
            [FindByXPath("span[text()='ALL NEWS']")]
            public Clickable<_, _> AllNewsButton { get; private set; }

            [ControlDefinition("div", ContainingClass = "other-news")]
            public ControlList<NewsContainer, _> OtherNewsList { get; private set; }

            [FindByXPath("span[text()='HIDE NEWS']")]
            public Clickable<_, _> HideNewsButton { get; private set; }

            public void ShowOtherNews()
            {
                AllNewsButton.Wait(Until.Visible);
                Retry.Exponential<Exception>(RepeatActionTimes, () =>
                {
                    AllNewsButton.ClickAndGo();
                    AllNewsButton.Should.Not.Exist();
                    OtherNewsList[0].Should.WithRetry.BeVisible();
                });
                
            }
        }
    }

    public BrandNews GetBrandNews(string brandName) => BrandsNews[el => el.BrandName.Value.ToLower().Contains(brandName.ToLower())];

    public AddNewsPage StartCompanyNewsAdd()
    {
        AddCompanyNewsButton.Wait(Until.Visible);
        var addCompanyNewsPage = AddCompanyNewsButton.ClickAndGo();
        Go.ToNextWindow(addCompanyNewsPage);
        return addCompanyNewsPage.CompanyNameInput.Wait(Until.Visible);
    }
}