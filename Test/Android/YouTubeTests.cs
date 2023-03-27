using OpenQA.Selenium.Appium.Android;
using Tests.UI;

namespace Mobile_test_project
{
    [TestClass]
    public class YouTubeTests : BaseAndroidTest
    {
        [TestMethod]
        public void YoutubeTest()
        {
            driver.PressKeyCode(AndroidKeyCode.Home);

            var youTubeIcon = driver.FindElementByXPath("//android.widget.TextView[@content-desc=\"YouTube\"]");
            youTubeIcon.Click();

            var pdateButton = driver.FindElementByXPath("//hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.TextView");
            pdateButton.Click();

            var signInButton = driver.FindElementByXPath("//hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.LinearLayout/android.widget.Button");
            Assert.AreEqual("Sign in", signInButton.Text, "Sign in button should have proper Label");
        }
    }
}