using System.Threading;
using Atata;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata.Extensions;
using UI.Business.Home;
using static Core.Constants.TestCategories;

namespace Tests.UI.Home;

[TestClass]
public class MainPageTests : BaseUiTest
{
    [TestMethod]
    [StoryId(1), TestCategory(SmokeUi)]
    public void HomePageInfoComponentsAndTabsSwitchTest()
    {
        const string expectedVideoTitle = "Warhammer 40,000 Mechanicus OST";

        var mainPage = GoWithRetry.To<MainPage>(url: Url.BaseAppUrl);
        mainPage.Videos[0].Wait(Until.Visible);

        Thread.Sleep(2000);

        mainPage.HeaderElements.SearchInput.Wait(Until.Visible);

        //  Check video search
        mainPage.HeaderElements.SearchInput.Set(expectedVideoTitle);
        mainPage.HeaderElements.SearchInput.Should.WithRetry.Contain(expectedVideoTitle);

        Thread.Sleep(2000);

        var searchResultPage = mainPage.HeaderElements.SearchIcon.ClickAndGo();

        searchResultPage.GetVideo(expectedVideoTitle).Wait(Until.Visible);

        Thread.Sleep(2000);

        //  Check opened video
        var playerPage = searchResultPage.GetVideo(expectedVideoTitle).Title.ClickAndGo();
        playerPage.VideoTitle.Wait(Until.Visible);

        Thread.Sleep(2000);

        Assert.IsTrue(playerPage.VideoTitle.Content.Value.Contains(expectedVideoTitle), "Expected video should be opened");
    }
}