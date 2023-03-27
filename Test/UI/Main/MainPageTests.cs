using Atata;
using Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Atata.Extensions;
using UI.Business.Home;
using static Core.Constants.TestCategories;

namespace Tests.UI.Main;

[TestClass]
public class MainPageTests : BaseUiTest
{
    [TestMethod]
    [StoryId(1), TestCategory(SmokeUi)]
    public void VideoSearchTest()
    {
        const string expectedVideoTitle = "Warhammer 40,000 Mechanicus OST";

        var mainPage = GoWithRetry.To<MainPage>(url: Url.BaseAppUrl);
        mainPage.Videos[0].Wait(Until.Visible);

        mainPage.HeaderElements.SearchInput.Wait(Until.Visible);

        //  Check video search
        mainPage.HeaderElements.SearchInput.Set(expectedVideoTitle);
        mainPage.HeaderElements.SearchInput.Should.WithRetry.Contain(expectedVideoTitle);

        var searchResultPage = mainPage.HeaderElements.SearchIcon.ClickAndGo();

        searchResultPage.GetVideo(expectedVideoTitle).Wait(Until.Visible);

        //  Check opened video
        var playerPage = searchResultPage.GetVideo(expectedVideoTitle).Title.ClickAndGo();
        playerPage.VideoTitle.Wait(Until.Visible);

        Assert.IsTrue(playerPage.VideoTitle.Content.Value.Contains(expectedVideoTitle), "Expected video should be opened");
    }
}