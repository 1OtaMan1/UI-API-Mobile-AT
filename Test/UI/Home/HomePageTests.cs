using System;
using System.Linq;
using Atata;
using Core.Helpers;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata;
using UI.Atata.Extensions;
using UI.Business.BaseApp.Home;
using static Core.Constants.Sizes;
using static Core.Constants.TestCategories;

namespace Tests.UI.Home;

[TestClass]
public class HomePageTests : BaseUiTest
{
    [TestMethod]
    [StoryId(46652), TestCategory(SmokeUi)]
    public void HomePageInfoComponentsAndTabsSwitchTest()
    {
        const string appDescription =
            "Ukrainians from all walks of life are fighting for the freedom and sovereignty of their country and protecting democracy for the world. And it’s time for all of us to do our part too! We must strengthen the impact of global sanctions by petitioning all international brands to stop business with russia. #stopbusinesswithrussia’s goal is to bring awareness to those brands who have taken the right step and ceased dealings with russia and to help global citizens to demand change from those yet to cut ties. The world says they stand with Ukraine, and actions speak louder than words. We are here to help make taking action possible.";
        const string infoTitle =
            "Give Ukraine your full support by petitioning all companies still doing business with Russia, and share this page with all your networks.";

        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        brandsPage.CompaniesCards[0].Wait(Until.Visible);

        //  Check home page main info
        brandsPage.AppDescription.Should.WithRetry.ContainIgnoringCase(appDescription);
        brandsPage.InitiativeMainInfo.Title.Should.ContainIgnoringCase(infoTitle);
        brandsPage.InitiativeMainInfo.SharesQty.Wait(Until.Visible);
        brandsPage.InitiativeMainInfo.SupportersQty.Wait(Until.Visible);
        brandsPage.InitiativeMainInfo.PetitionAllButton.Wait(Until.Visible);

        //  Check tabs switch work
        var newsTab = brandsPage.SwitchToNewsTab();
        newsTab.BrandsNews[0].Wait(Until.Visible);
        newsTab.SwitchToBrandsTab();
        brandsPage.CompaniesCards[0].Wait(Until.Visible);
    }

    [TestMethod]
    [StoryId(46652), TestCategory(SmokeUi)]
    public void CompaniesTabFunctionalTest()
    {
        #region Setup

        const string companyName = "Bayer";
        const string searchQuery = "er";
        const string callToAction =
            "If you know that they are still doing business in russia, you can suggest we add them to our list";
        var incorrectSearchQuery = Generator.Text(15);

        #endregion

        //  Check companies cards displayed
        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        brandsPage.CompaniesCards[0].Wait(Until.Visible);
        AtataUtils.ScrollToTheBottom();

        var companyCard = brandsPage.GetCompany(companyName);

        //  Check possibility to start petition against certain company
        companyCard.Wait(Until.Visible);
        var petitionCompanyPage = companyCard.StartPetitionSign();

        petitionCompanyPage.CompanyInfo.Wait(Until.Visible);
        petitionCompanyPage.CloseWindow();

        //  Check share company functional
        companyCard.ShareIcon.ClickAndGo();

        companyCard.SocialMediasTooltip.LinkIcon.Wait(Until.Visible);
        companyCard.SocialMediasTooltip.LinkIcon.ClickAndGo();
        brandsPage.SearchInput.Click();
        AtataUtils.EmulatePasteAction();
        var url = brandsPage.Wait(OneItem).SearchInput.Get();

        AtataUtils.RestartDriver();

        brandsPage = GoWithRetry.To(brandsPage, url);

        brandsPage.CompaniesCards[0].Wait(Until.Visible);
        brandsPage.CompaniesCards[0].CompanyName.Should.WithRetry.ContainIgnoringCase(companyName);
        brandsPage.CompaniesCards[0].Highlight.Wait(Until.Visible);

        //  Check search functional
        GoWithRetry.To(brandsPage, Url.BaseAppUrl);
        brandsPage.SearchInput.Wait(Until.Visible);
        brandsPage.SearchInput.Set(searchQuery);

        brandsPage.CompaniesCards[0].Wait(Until.Visible);
        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () =>
            Assert.IsTrue(brandsPage.CompaniesCards.All(card => card.CompanyName.Value.ToLower().Contains(searchQuery)),
                "Company search should work properly"));

        //  Check no search result found
        brandsPage.SearchInput.Set(incorrectSearchQuery);
        brandsPage.EmptyState.Wait(Until.Visible);

        brandsPage.EmptyState.Should.ContainIgnoringCase(callToAction);

        //  Check possibility to start adding new company
        var addCompanyPage = brandsPage.StartCompanyAdd();

        addCompanyPage.CeoNameInput.Wait(Until.Visible);
    }

    [TestMethod]
    [StoryId(46652), TestCategory(SmokeUi)]
    public void HomePageCompanyCardTest()
    {
        const string companyName = "COTY";
        const string ceoName = "Sue Y. Nabi";

        //  Check company name
        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var companyCard = brandsPage.GetCompany(companyName);

        companyCard.Wait(Until.Visible);

        //  Check company logo
        companyCard.Logo.Wait(Until.Visible);

        //  Check ceo name
        companyCard.CEOName.Should.ContainIgnoringCase(ceoName);

        //  Check signed petitions count
        companyCard.SignedPetitionsQty.Wait(Until.Visible);

        //  Check sharing possibility content
        companyCard.ShareIcon.ClickAndGo();
        companyCard.SocialMediasTooltip.Wait(Until.Visible);

        companyCard.SocialMediasTooltip.FacebookIcon.Wait(Until.Visible);
        companyCard.SocialMediasTooltip.TwitterIcon.Wait(Until.Visible);
        companyCard.SocialMediasTooltip.LinkedInIcon.Wait(Until.Visible);
        companyCard.SocialMediasTooltip.EmailIcon.Wait(Until.Visible);

        //  Check add news about company possibility
        var newsPage = brandsPage.SwitchToNewsTab();
        var companyNewsPage = newsPage.StartCompanyNewsAdd();

        companyNewsPage.FirstNewsUrlInput.Wait(Until.Visible);
    }

    [TestMethod]
    [StoryId(46653), TestCategory(SmokeUi)]
    public void NewsItemTest()
    {
        const string brandName = "AIRBNB";
        const string secondBrandName = "ADIDAS";

        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var newsPage = brandsPage.SwitchToNewsTab();

        //  Check news dates
        Retry.Exponential<Exception>(RepeatActionTimes, () =>
        {
            AtataUtils.ScrollToTheBottom();
            newsPage.GetBrandNews(secondBrandName).Wait(Until.Visible);
        });

        var lastNewsDate = DateTime.ParseExact(newsPage.GetBrandNews(brandName).NewsDate.Value, "dd/MM/yyyy", null);
        var previousNewsDate = DateTime.ParseExact(newsPage.GetBrandNews(secondBrandName).NewsDate.Value, "dd/MM/yyyy", null);

        Assert.IsTrue(previousNewsDate < lastNewsDate, "Last news should be on top of list");

        //  Check company name and logo displayed
        var brandNews = newsPage.GetBrandNews(brandName);
        brandNews.BrandImage.Wait(Until.Visible);

        // Check brand news and news date
        brandNews.LatestNews.Date.Wait(Until.Visible);
        var externalPage = brandNews.LatestNews.News.ClickAndGo();
        Go.ToNextWindow(externalPage);
        externalPage.Body.Wait(Until.Visible);
        externalPage.CloseWindow();

        //  Check possibility to like and like counter
        brandNews.LikeIcon.Wait(Until.Visible);
        brandNews.LikeCounter.Wait(Until.Visible);
        var likeCounter = Convert.ToInt32(brandNews.LikeCounter.Value);
        brandNews.LikeIcon.ClickAndGo();
        var expectedLikeCounter = likeCounter + 1;

        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () =>
            Assert.AreEqual(expectedLikeCounter, Convert.ToInt32(brandNews.LikeCounter.Value), "The like counter should be increased"));

        //  Check shares counter value update
        var shareCounter = Convert.ToInt32(brandNews.ShareCounter.Value);
        brandNews.ShareIcon.Hover();
        brandNews.SocialMediasTooltip.Wait(Until.Visible);
        brandNews.SocialMediasTooltip.LinkIcon.ClickAndGo();
        newsPage.SearchInput.Click();
        AtataUtils.EmulatePasteAction();
        var url = newsPage.Wait(OneItem).SearchInput.Get();
        var expectedShareCounter = shareCounter + 1;

        AtataUtils.RestartDriver();

        newsPage = GoWithRetry.To(newsPage, url);
        newsPage.BrandsNews[0].Wait(Until.Visible);
        brandNews = newsPage.GetBrandNews(brandName);
        brandNews.ShareCounter.Wait(Until.Visible);

        Retry.Exponential<AssertFailedException>(RepeatActionTimes, () =>
             Assert.AreEqual(expectedShareCounter, Convert.ToInt32(brandNews.ShareCounter.Value), "The share counter should be increased"));
    }

    [TestMethod]
    [StoryId(46653), TestCategory(SmokeUi)]
    public void NewsListDropdownTest()
    {
        const string brandName = "AIRBNB";

        var brandsPage = GoWithRetry.To<BrandsTab>(url: Url.BaseAppUrl);
        var newsPage = brandsPage.SwitchToNewsTab();
        var brandNews = newsPage.GetBrandNews(brandName);
        brandNews.Wait(Until.Visible);

        // Check All news button functionality
        brandNews.OtherNews.ShowOtherNews();

        brandNews.LatestNews.News.Wait(Until.Visible);
        brandNews.OtherNews.OtherNewsList[0].News.Wait(Until.Visible);
        brandNews.OtherNews.AllNewsButton.Wait(Until.MissingOrHidden);

        // Check brand news dates
        var latestNewsDate = DateTime.ParseExact(brandNews.LatestNews.Date.Value, "dd/MM/yyyy", null);
        var otherNewsDate = DateTime.ParseExact(brandNews.OtherNews.OtherNewsList[0].Date.Value, "dd/MM/yyyy", null);

        Assert.IsTrue(otherNewsDate <= latestNewsDate, "Last news should be on top of list");

        // Check redirection after clicking on the latest news title
        var externalPageLatestNews = brandNews.LatestNews.News.ClickAndGo();
        Go.ToNextWindow(externalPageLatestNews);
        externalPageLatestNews.Body.Wait(Until.Visible);
        externalPageLatestNews.CloseWindow();

        // Check redirection after clicking on the other news title
        var externalPageOtherNews = brandNews.OtherNews.OtherNewsList[0].News.ClickAndGo();
        Go.ToNextWindow(externalPageOtherNews);
        externalPageOtherNews.Body.Wait(Until.Visible);
        externalPageOtherNews.CloseWindow();

        // Check Hide news button functionality
        brandNews.OtherNews.HideNewsButton.Wait(Until.Visible);
        brandNews.OtherNews.HideNewsButton.ClickAndGo();

        brandNews.OtherNews.OtherNewsList[0].News.Wait(Until.MissingOrHidden);
        brandNews.OtherNews.HideNewsButton.Wait(Until.MissingOrHidden);
    }
}