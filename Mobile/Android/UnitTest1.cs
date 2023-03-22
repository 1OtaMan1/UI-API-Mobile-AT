using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Mobile_test_project
{
    [TestClass]
    public class UnitTest1
    {
        public AndroidDriver<AppiumWebElement> driver = GetDriver();

        public static AndroidDriver<AppiumWebElement> GetDriver()
        {
            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AddAdditionalCapability("deviceName", "sdk_gpphone_x86");
            options.AddAdditionalCapability("platformVersion", "11");

            return new AndroidDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), options);
        }

        [TestInitialize]
        public void StartScreenRecord()
        {
            driver.StartRecordingScreen();
        }

        [TestCleanup]
        public void StopScreenRecord()
        {
            var video = driver.StopRecordingScreen();
            byte[] ret = Convert.FromBase64String(video);
            FileInfo file = new($"H:\\videoRecords\\{DateTime.Now.ToString("yyyyMMddTHHmmss")}.mp4");
            using Stream sw = file.OpenWrite();
            sw.Write(ret, 0, ret.Length);
            sw.Close();
        }

        [TestMethod]
        public void YoutubeTest()
        {
            driver.PressKeyCode(AndroidKeyCode.Home);

            Thread.Sleep(2000);
            var youTubeIcon = driver.FindElementByXPath("//android.widget.TextView[@content-desc=\"YouTube\"]");

            Thread.Sleep(2000);
            youTubeIcon.Click();
            Thread.Sleep(2000);

            var pdateButton = driver.FindElementByXPath("//hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.TextView");
            pdateButton.Click();
            Thread.Sleep(2000);

            var signInButton = driver.FindElementByXPath("//hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.LinearLayout/android.widget.Button");
            Assert.AreEqual("Sign in", signInButton.Text, "Sign in button should have proper Label");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var backButton = driver.FindElementByXPath("//android.widget.ImageButton[@content-desc=\"Back\"]");

            Thread.Sleep(2000);
            backButton.Click();
            Thread.Sleep(2000);

            var privacyMenuOption = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.view.ViewGroup/android.widget.ScrollView/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/androidx.recyclerview.widget.RecyclerView/android.widget.LinearLayout[2]/android.widget.LinearLayout/android.widget.ImageView");

            privacyMenuOption.Click();
            Thread.Sleep(2000);
            
        }
    }
}